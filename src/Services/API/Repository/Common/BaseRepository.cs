using API.Common;
using API.Common.Queries;
using API.Domain.Database;
using API.Domain.Table;
using API.Domain.View;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace API.Repository.Common
{
  
/// <summary>
    /// Base repository class providing common database operations.
    /// </summary>
    public class BaseRepository : IBaseRepository
    {
        internal string _connectionString;
    /// <summary>
    /// 
    /// </summary>
        public string? CurrentDatabases { get { return _sqlConnectionStringBuilder?.InitialCatalog; } }
    /// <summary>
    /// 
    /// </summary>
        public string? DataSource { get { return _sqlConnectionStringBuilder?.DataSource; } }

        private SqlConnectionStringBuilder _sqlConnectionStringBuilder;
    /// <summary>
    /// 
    /// </summary>
        public DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions()
                  .SetSlidingExpiration(TimeSpan.FromMinutes(60)) // Set expiration time for cache
            .SetAbsoluteExpiration(TimeSpan.FromHours(24)); // Optional absolute expiration
    /// <summary>
    /// 
    /// </summary>
     public readonly IDistributedCache _cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="cache">The distributed cache instance.</param>
        public BaseRepository(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
        }

        /// <summary>
        /// Gets a database connection for the specified database name.
        /// </summary>
        /// <param name="currentDbName">The name of the database.</param>
        /// <returns>An <see cref="IDbConnection"/> instance.</returns>
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

        /// <summary>
        /// Processes metadata and groups it by table name.
        /// </summary>
        /// <param name="metadataList">The metadata list.</param>
        /// <returns>A list of <see cref="TablesMetadata"/> instances.</returns>
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

        /// <summary>
        /// Gets a database connection.
        /// </summary>
        /// <returns>An <see cref="IDbConnection"/> instance.</returns>
        public IDbConnection GetDbConnection() { return new SqlConnection(_connectionString); }

        /// <summary>
        /// Loads advanced server settings from cache or queries the database.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A collection of <see cref="ServerProperty"/> instances.</returns>
        public async Task<IEnumerable<ServerProperty>> LoadAdvancedServerSettingsAsync(IDbConnection connection = null)
        {
            return await LoadFromCacheOrQueryAsync<ServerProperty>(
                CacheConstants.DatabaseCache.AdvancedServerSettings,
                SqlQueryConstant.LoadAdvancedServerSettings,
                connection);
        }

        /// <summary>
        /// Loads aggregate functions from cache or queries the database.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <returns>A collection of <see cref="FunctionInfo"/> instances.</returns>
        public async Task<IEnumerable<FunctionInfo>> LoadAggregateFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.AggregateFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstant.LoadAggregateFunctions,
                GetDbConnection(databaseName));
        }

        /// <summary>
        /// Loads the name of the current database.
        /// </summary>
        /// <returns>The name of the current database.</returns>
        public string GetCurrentDatabaseName()
        {
            return CurrentDatabases ?? string.Empty;
        }

        /// <summary>
        /// Loads databases from cache or queries the database.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A collection of <see cref="DatabaseInfo"/> instances.</returns>
        public async Task<IEnumerable<DatabaseInfo>> LoadDatabases(IDbConnection connection = null)
        {
            return await LoadFromCacheOrQueryAsync<DatabaseInfo>(
                CacheConstants.DatabaseCache.DatabaseNames,
                SqlQueryConstant.LoadDatabases,
                connection);
        }

        /// <summary>
        /// Loads the name of the database server.
        /// </summary>
        /// <returns>The name of the database server.</returns>
        public string LoadDatabaseServerName()
        {
            return DataSource;
        }

        /// <summary>
        /// Loads database triggers from cache or queries the database.
        /// </summary>
        /// <param name="currentDbName">The name of the current database.</param>
        /// <returns>A collection of <see cref="TriggerInfo"/> instances.</returns>
        public async Task<IEnumerable<TriggerInfo>> LoadDatabaseTriggersAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<TriggerInfo>(
                CacheConstants.DatabaseCache.DatabaseTriggers +
                    (!string.IsNullOrEmpty(currentDbName) ? currentDbName : string.Empty),
                SqlQueryConstant.LoadDatabaseTriggers,
                GetDbConnection(currentDbName));
        }

        /// <summary>
        /// Loads data from cache or queries the database.
        /// </summary>
        /// <typeparam name="T">The type of data to load.</typeparam>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="sqlquery">The SQL query.</param>
        /// <param name="dbConnections">The database connection.</param>
        /// <returns>A collection of <typeparamref name="T"/> instances.</returns>
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

        /// <summary>
        /// Loads scalar functions from cache or queries the database.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <returns>A collection of <see cref="FunctionInfo"/> instances.</returns>
        public async Task<IEnumerable<FunctionInfo>> LoadScalarFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.ScalarFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstant.LoadScalarFunctions,
                GetDbConnection(databaseName));
        }

        /// <summary>
        /// Loads server properties from cache or queries the database.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A collection of <see cref="ServerProperty"/> instances.</returns>
        public async Task<IEnumerable<ServerProperty>> LoadServerPropertiesAsync(IDbConnection connection = null)
        {
            if (connection == null)
            {
                connection = GetDbConnection();
            }

            try
            {
                var properties = await connection.QuerySingleOrDefaultAsync(SqlQueryConstant.LoadServerProperties);

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

        /// <summary>
        /// Loads stored procedures from cache or queries the database.
        /// </summary>
        /// <param name="currentDbName">The name of the current database.</param>
        /// <returns>A collection of <see cref="ProcedureInfo"/> instances.</returns>
        public async Task<IEnumerable<ProcedureInfo>> LoadStoredProceduresAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<ProcedureInfo>(
                CacheConstants.DatabaseCache.StoredProcedures +
                    (!string.IsNullOrEmpty(currentDbName) ? currentDbName : string.Empty),
                SqlQueryConstant.LoadStoredProcedures,
                GetDbConnection(currentDbName));
        }

        /// <summary>
        /// Loads tables metadata from cache or queries the database.
        /// </summary>
        /// <param name="currentDbName">The name of the current database.</param>
        /// <returns>A collection of <see cref="TablesMetadata"/> instances.</returns>
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
                    SqlQueryConstant.GetAllTablesExtendedProperties);
                var tableResult = ProcessMetadata(tableDetails);
                var serializedData = JsonSerializer.Serialize(tableResult);
                return tableResult;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<TablesMetadata>(); // Return empty collection to avoid null issues
            }
        }

        /// <summary>
        /// Loads table-valued functions from cache or queries the database.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <returns>A collection of <see cref="FunctionInfo"/> instances.</returns>
        public async Task<IEnumerable<FunctionInfo>> LoadTableValuedFunctionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<FunctionInfo>(
                CacheConstants.DatabaseCache.TableValuedFunctions +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstant.LoadTableValuedFunctions,
                GetDbConnection(databaseName));
        }

        /// <summary>
        /// Loads user-defined data types from cache or queries the database.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <returns>A collection of <see cref="UserType"/> instances.</returns>
        public async Task<IEnumerable<UserType>> LoadUserDefinedDataTypesAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<UserType>(
                CacheConstants.DatabaseCache.UserDefinedDataTypes +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstant.LoadUserDefinedDataTypes,
                GetDbConnection(databaseName));
        }

        /// <summary>
        /// Loads view metadata from cache or queries the database.
        /// </summary>
        /// <param name="currentDbName">The name of the current database.</param>
        /// <returns>A collection of <see cref="ViewMetadata"/> instances.</returns>
        public async Task<IEnumerable<ViewMetadata>> LoadViewAsync(string currentDbName = null)
        {
            return await LoadFromCacheOrQueryAsync<ViewMetadata>(
                CacheConstants.DatabaseCache.ViewDetails,
                SqlQueryConstant.LoadViewDetails,
                currentDbName == null || currentDbName == string.Empty
                    ? GetDbConnection()
                    : GetDbConnection(currentDbName));
        }

        /// <summary>
        /// Loads XML schema collections from cache or queries the database.
        /// </summary>
        /// <param name="databaseName">The name of the database.</param>
        /// <returns>A collection of <see cref="DbXmlSchema"/> instances.</returns>
        public async Task<IEnumerable<DbXmlSchema>> LoadXmlSchemaCollectionsAsync(string databaseName = null)
        {
            return await LoadFromCacheOrQueryAsync<DbXmlSchema>(
                CacheConstants.DatabaseCache.XmlSchemaCollections +
                    (!string.IsNullOrEmpty(databaseName) ? databaseName : string.Empty),
                SqlQueryConstant.LoadXmlSchemaCollections,
                GetDbConnection(databaseName));
        }

        /// <summary>
        /// Gets detailed view information from cache or queries the database.
        /// </summary>
        /// <returns>A collection of <see cref="ViewDetails"/> instances.</returns>
        public async Task<IEnumerable<ViewDetails>> GetDetailedViewsInfoAsync()
        {
            IEnumerable<ViewDetails> viewDetails = null;
            var cacheKey = $"ViewsKeys" + CurrentDatabases;
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<IEnumerable<ViewDetails>>(cachedData);
            }
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    viewDetails = await db.QueryAsync<ViewDetails>(SqlQueryConstant.GetAllViewsDetailsWithMsDesc);
                    return viewDetails;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
