using API.Common;
using API.Model.Procedure;
using API.Repository.Common;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;

namespace API.Repository.Procedure.Repository
{
    public class ProcedureRepository : BaseRepository
    {
        readonly string _connectionString;
        private readonly IDbConnection _dbConnection;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _redisCache;
        private readonly ILogger<ProcedureRepository> _logger;
        private string _procedureName { get; set; }
        private bool _useMemoryCache { get; set; }
        public ProcedureRepository(string connectionString, ILogger<ProcedureRepository> logger, IDistributedCache redisCache) : base(connectionString, redisCache)
        {
            _connectionString = connectionString;
            _dbConnection = GetDbConnection();
            _redisCache = redisCache;
            _logger = logger;
        }
        public async Task<ProcedureMetadata> LoadProcedureMetaData(string procedureName, bool useMemoryCache = false)
        {
            _procedureName = procedureName;
            _useMemoryCache = useMemoryCache;
            var procedureMetadata = new ProcedureMetadata();

            procedureMetadata.executionPlan = await GetExecutionPlanAsync();

            return procedureMetadata;
        }
        private async Task<ExecutionPlan> GetExecutionPlanAsync()
        {
            var cacheKey = CacheConstants.procedureCache.StoreProcedureExecutionPlanCache.Replace("@storedProcName", _procedureName);
            ExecutionPlan executionPlan = new ExecutionPlan();

            // Check MemoryCache first if enabled
            if (_useMemoryCache && _memoryCache.TryGetValue(cacheKey, out executionPlan))
            {
                _logger.LogInformation("Retrieved execution plan from MemoryCache.");
                return executionPlan;
            }

            // If not found in MemoryCache, check Redis cache
            if (!_useMemoryCache)
            {
                //return JsonSerializer.Deserialize<TableMetadata>(cachedData);
                var CachedexecutionPlan = await _redisCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(CachedexecutionPlan))
                {
                    _logger.LogInformation("Retrieved execution plan from Redis cache.");
                    return JsonSerializer.Deserialize<ExecutionPlan>(CachedexecutionPlan);
                }
            }

            // If not found in either cache, fetch from the database
            executionPlan = await FetchExecutionPlanFromDb(_procedureName);

            // If the first attempt did not yield a valid execution plan, try again
            if (executionPlan == null || (string.IsNullOrEmpty(executionPlan.QueryPlanXml)))
            {
                _logger.LogWarning("Initial fetch from the database did not return a valid execution plan. Retrying...");
                executionPlan = await FetchExecutionPlanFromDb(_procedureName);
            }
            // Cache the result in MemoryCache if enabled
            if (_useMemoryCache)
            {
                _memoryCache.Set(cacheKey, executionPlan, cacheEntryOptions.AbsoluteExpirationRelativeToNow.GetValueOrDefault());
                _logger.LogInformation("Cached execution plan in MemoryCache.");
            }
            else // Otherwise, cache in Redis
            {
                if (executionPlan != null)
                {
                    var serializedData = JsonSerializer.Serialize(executionPlan);
                    await _redisCache.SetStringAsync(cacheKey, serializedData, cacheEntryOptions);
                    _logger.LogInformation("Cached execution plan in Redis cache.");
                }
            }

            return executionPlan;
        }
        private async Task<ExecutionPlan> FetchExecutionPlanFromDb(string procName)
        {
            try
            {
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    await dbConnection.OpenAsync();
                    var query = SqlQueryConstants.ProcedureQuery.GetExecutionPlanOfStoreProc.Replace("@procName", "'" + procName + "'");

                    Console.WriteLine("Executing query: " + query);

                    var QueryResult = await dbConnection.QueryAsync<string>(query);

                    var resultList = QueryResult.ToList();

                    if (resultList.Count == 0)
                    {
                        Console.WriteLine("No execution plans found.");
                        return new ExecutionPlan();
                    }
                    else if (resultList.Count == 1)
                    {
                        return new ExecutionPlan() { QueryPlanXml = resultList.First() };
                    }
                    else
                    {
                        return new ExecutionPlan() { QueryPlanXml = resultList.Last() };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the execution plan from the database.");
            }
            finally
            {
                _dbConnection.Close();
            }

            return new ExecutionPlan();
        }
    }
}
