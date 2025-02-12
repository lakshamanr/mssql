using API.Common;
using API.Common.Queries;
using API.Domain.Database;
using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace API.Repository.Database
{
  /// <summary>
  /// 
  /// </summary>
    public class DatabaseReposititory : BaseRepository, IDatabaseReposititory
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="logger"></param>
       /// <param name="cache"></param>
       /// <param name="configuration"></param>
        public DatabaseReposititory(ILogger<DatabaseReposititory> logger, IDistributedCache cache, IConfiguration configuration) : base(cache, configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }
        //      Task<DatabaseMetaData> GetDatabaseMetaData();
        /// <summary>
        /// Gets the metadata of the database.
        /// </summary>
        /// <returns>A <see cref="DatabaseMetaData"/> instance.</returns>
        public async Task<DatabaseMetaData> GetDatabaseMetaData()
        {
            DatabaseMetaData serverMetaData;
            var cachedData = await _cache.GetStringAsync(CacheConstants.DatabaseCache.ServerMetaDataCacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                serverMetaData = JsonSerializer.Deserialize<DatabaseMetaData>(cachedData) ?? new DatabaseMetaData();
            }
            else
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    serverMetaData = new DatabaseMetaData();
                    serverMetaData.CurrentDatabaseName = GetCurrentDatabaseName();
                    serverMetaData.DatabaseInfos = await LoadDatabases(db);
                    serverMetaData.DatabaseServerName = LoadDatabaseServerName();
                    serverMetaData.ProcedureInfos = await LoadStoredProceduresAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.ScalarFunctionInfos = await LoadScalarFunctionsAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.TableFunctionInfos = await LoadTableValuedFunctionsAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.UserTypes = await LoadUserDefinedDataTypesAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.DbXmlSchemas = await LoadXmlSchemaCollectionsAsync(serverMetaData.CurrentDatabaseName);
                    serverMetaData.ServerProperties = await LoadServerPropertiesAsync(db);
                    serverMetaData.ServerAdvanceProperties = await LoadAdvancedServerSettingsAsync(db);
                    serverMetaData.fileInformations = await LoadDatabaseFiles(db);
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
        /// <summary>
        /// Loads database files from cache or queries the database.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A collection of <see cref="DatabaseFile"/> instances.</returns>
        private async Task<IEnumerable<DatabaseFile>> LoadDatabaseFiles(IDbConnection connection)
        {
            return await LoadFromCacheOrQueryAsync<DatabaseFile>(
                CacheConstants.DatabaseCache.DatabaseFiles,
                SqlQueryConstant.LoadDatabaseFiles
                    .Replace("@DatabaseName", $"'{CurrentDatabases}'"),
                connection);
        }

    }
}
