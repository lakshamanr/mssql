using API.Common;
using API.Domain.Database;
using API.Domain.Table;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace API.Repository.Common
{
    public class BaseRepository : IBaseRepository
    {
        private readonly string _connectionString;
        private SqlConnectionStringBuilder _SqlConnectionStringBuilder;

        DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromMinutes(60)) // Set expiration time for cache
            .SetAbsoluteExpiration(TimeSpan.FromHours(24)); // Optional absolute expiration
        public readonly IDistributedCache _cache;

        public BaseRepository(string connectionString, IDistributedCache cache)
        {
            _cache = cache;
            _connectionString = connectionString;
            _SqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        private IDbConnection GetDbConnection(string currentDbName)
        {
            if (string.IsNullOrEmpty(currentDbName))
            {
                return GetDbConnection();
            }

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            connectionStringBuilder.InitialCatalog = currentDbName;
            IDbConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            return connection;
        }

        private async Task<IEnumerable<DatabaseFile>> LoadDatabaseFiles(IDbConnection connection = null)
        {
            return await LoadFromCacheOrQueryAsync<DatabaseFile>(
                CacheConstants.DatabaseCache.DatabaseFiles,
                SqlQueryConstants.DatabaseQuery.LoadDatabaseFiles
                    .Replace("@DatabaseName", $"'{_SqlConnectionStringBuilder.InitialCatalog}'"),
                connection);
        }

        private List<TablesMetadata> ProcessMetadata(IEnumerable<TablesMetadata> metadataList)
        {
            var result = metadataList.GroupBy(item => item.TableName)
                .ToDictionary(
                    group => group.Key,
                    group =>
                    {
                        var table = new TablesMetadata
                        {
                            TableName = group.Key,
                            tableColumns = new List<TableColumns>()
                        };

                        foreach (var item in group)
                        {
                            if (item.Level != "Table")
                            {
                                table.tableColumns
                                    .Add(
                                        new TableColumns
                                        {
                                            ColumnName = item.ColumnName,
                                            ExtendedPropertyName = item.Name,
                                            ExtendedPropertyValue = item.Value
                                        });
                            }
                            else
                            {
                                table.ExtendedPropertyName = item.Name;
                                table.ExtendedPropertyValue = item.Value;
                            }
                        }

                        return table;
                    });

            return result.Values.ToList();
        }

        public async Task<DatabaseMetaData> GetDatabaseMetaData()
        {
            DatabaseMetaData serverMetaData;
            var cachedData = await _cache.GetStringAsync(CacheConstants.DatabaseCache.ServerMetaDataCacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                serverMetaData = JsonSerializer.Deserialize<DatabaseMetaData>(cachedData);
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    serverMetaData = new DatabaseMetaData();
                    serverMetaData.CurrentDatabaseName = LoadDatabaseName();
                    serverMetaData.DatabaseInfos = await LoadDatabases(db);
                    serverMetaData.DatabaseServerName = LoadDatabaseServerName();
                    serverMetaData.ProcedureInfos = await LoadStoredProceduresAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.ScalarFunctionInfos = await LoadScalarFunctionsAsync(
                        serverMetaData.CurrentDatabaseName);
                    serverMetaData.TableFunctionInfos = await LoadTableValuedFunctionsAsync(
                        serverMetaData.CurrentDatabaseName);
                    serverMetaData.UserTypes = await LoadUserDefinedDataTypesAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.DbXmlSchemas = await LoadXmlSchemaCollectionsAsync(
                        serverMetaData.CurrentDatabaseName);
                    serverMetaData.ServerProperties = await LoadServerPropertiesAsync(db);
                    serverMetaData.ServerAdvanceProperties = await LoadAdvancedServerSettingsAsync(db);
                    serverMetaData.fileInfomrations = await LoadDatabaseFiles();
                    serverMetaData.viewMetadata = await LoadViewAsync();
                    serverMetaData.tablesMetadata = await LoadTablesAsync();
                    serverMetaData.TriggerInfos = await LoadDatabaseTriggersAsync(serverMetaData.CurrentDatabaseName);
                }
                await _cache.SetStringAsync(
                    CacheConstants.DatabaseCache.ServerMetaDataCacheKey,
                    JsonSerializer.Serialize(serverMetaData),
                    cacheEntryOptions);
            }

            return serverMetaData;
        }

        public IDbConnection GetDbConnection() { return new SqlConnection(_connectionString); }

        public async Task<IEnumerable<ServerProperty>> LoadAdvancedServerSettingsAsync(IDbConnection connection = null)
        {
            return await LoadFromCacheOrQueryAsync<ServerProperty>(
                CacheConstants.DatabaseCache.AdvancedServerSettings,
                SqlQueryConstants.DatabaseQuery.LoadAdvancedServerSettings,
                connection);
        }
        public async Task<IEnumerable<FunctionInfo>> LoadAggregateFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.AggregateFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadAggregateFunctions,
                GetDbConnection(databaseName));
        }
        public string LoadDatabaseName() => _SqlConnectionStringBuilder.InitialCatalog;

        public async Task<IEnumerable<DatabaseInfo>> LoadDatabases(IDbConnection connection = null)
        {
            return await LoadFromCacheOrQueryAsync<DatabaseInfo>(
                CacheConstants.DatabaseCache.DatabaseNames,
                SqlQueryConstants.DatabaseQuery.LoadDatabases,
                connection);
        }

        public string LoadDatabaseServerName() => _SqlConnectionStringBuilder.DataSource;

        public async Task<IEnumerable<TriggerInfo>> LoadDatabaseTriggersAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<TriggerInfo>(
                CacheConstants.DatabaseCache.DatabaseTriggers +
                    (!string.IsNullOrEmpty(currentDbName) ? currentDbName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadDatabaseTriggers,
                GetDbConnection(currentDbName));
        }

        public async Task<IEnumerable<T>> LoadFromCacheOrQueryAsync<T>(
            string cacheKey,
            string sqlquery,
            IDbConnection dbConnections = null)
        {
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<T>>(cachedData);
            }
            if (dbConnections == null)
            {
                dbConnections = GetDbConnection();
            }

            IEnumerable<T> dbData = await dbConnections.QueryAsync<T>(sqlquery);

            if (dbData.Any())
            {
                // Serialize and cache the data
                var serializedData = JsonSerializer.Serialize(dbData);
                await _cache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions);
            }
            return dbData;
        }

        public async Task<IEnumerable<FunctionInfo>> LoadScalarFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.ScalarFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadScalarFunctions,
                GetDbConnection(databaseName));
        }

        public async Task<IEnumerable<ServerProperty>> LoadServerPropertiesAsync(IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = GetDbConnection();
            }

            try
            {
                var properties = await connection.QuerySingleOrDefaultAsync(SqlQueryConstants.DatabaseQuery.LoadServerProperties);

                if (properties == null)
                {
                    return Enumerable.Empty<ServerProperty>();
                }

                // Build the server properties list
                var serverProperties = new List<ServerProperty>
                {
                    new ServerProperty { Name = "ProductName", Value = properties.ProductName },
                    new ServerProperty { Name = "ProductMajorVersion", Value = properties.ProductMajorVersion },
                    new ServerProperty { Name = "ProductBuild", Value = properties.ProductBuild },
                    new ServerProperty { Name = "InstanceDefaultLogPath", Value = properties.InstanceDefaultLogPath },
                    new ServerProperty { Name = "Edition", Value = properties.Edition },
                    new ServerProperty { Name = "BuildClrVersion", Value = properties.BuildClrVersion },
                    new ServerProperty { Name = "Collation", Value = properties.Collation },
                    new ServerProperty
                    {
                        Name = "ComputerNamePhysicalNetBIOS",
                        Value = properties.ComputerNamePhysicalNetBIOS
                    },
                    new ServerProperty { Name = "EngineEdition", Value = properties.EngineEdition },
                    new ServerProperty { Name = "Language", Value = properties.Language },
                    new ServerProperty { Name = "Platform", Value = properties.Platform },
                    new ServerProperty { Name = "IsClustered", Value = properties.IsClustered }
                };
                return serverProperties;
            }
            finally
            {
            }
        }

        public async Task<IEnumerable<ProcedureInfo>> LoadStoredProceduresAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<ProcedureInfo>(
                CacheConstants.DatabaseCache.StoredProcedures +
                    (!string.IsNullOrEmpty(currentDbName) ? currentDbName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadStoredProcedures,
                GetDbConnection(currentDbName));
        }

        public async Task<IEnumerable<TablesMetadata>> LoadTablesAsync()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
            return await LoadTablesAsync(sqlConnectionStringBuilder.InitialCatalog);
        }

        public async Task<IEnumerable<TablesMetadata>> LoadTablesAsync(string currentDbName = null)
        {
            var DatabaseTables = CacheConstants.DatabaseCache.DatabaseTables + currentDbName;

            var cachedData = await _cache.GetStringAsync(DatabaseTables);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<TablesMetadata>>(cachedData);
            }
            try
            {
                IDbConnection connection = GetDbConnection(currentDbName);
                var tableDetails = await connection.QueryAsync<TablesMetadata>(
                    SqlQueryConstants.TableQuery.LoadTablesExtendedProperties);
                var tableResult = ProcessMetadata(tableDetails);
                var serializedData = JsonSerializer.Serialize(tableResult);
                return tableResult;
            }
            catch (Exception)
            {
                return Enumerable.Empty<TablesMetadata>(); // Return empty collection to avoid null issues
            }
        }

        public async Task<IEnumerable<FunctionInfo>> LoadTableValuedFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.TableValuedFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadTableValuedFunctions,
                GetDbConnection(databaseName));
        }
        public async Task<IEnumerable<UserType>> LoadUserDefinedDataTypesAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<UserType>(
                CacheConstants.DatabaseCache.UserDefinedDataTypes +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadUserDefinedDataTypes,
                GetDbConnection(databaseName));
        }

        public async Task<IEnumerable<ViewMetadata>> LoadViewAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<ViewMetadata>(
                CacheConstants.DatabaseCache.ViewDetails,
                SqlQueryConstants.DatabaseQuery.LoadViewDetails,
                currentDbName == null || currentDbName == string.Empty
                    ? GetDbConnection()
                    : GetDbConnection(currentDbName));
        }

        public async Task<IEnumerable<DbXmlSchema>> LoadXmlSchemaCollectionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<DbXmlSchema>(
                CacheConstants.DatabaseCache.XmlSchemaCollections +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstants.DatabaseQuery.LoadXmlSchemaCollections,
                GetDbConnection(databaseName));
        }
    }
}
