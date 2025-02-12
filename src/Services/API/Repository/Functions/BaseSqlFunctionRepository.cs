using API.Common.Queries;
using API.Domain.Functions;
using API.Repository.Common;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using System.Data.SqlClient;

namespace API.Repository.Functions
{
    /// <summary>
    /// Provides a base repository for SQL functions, including methods to retrieve metadata, parameters, definitions, and dependencies.
    /// </summary>
    public class BaseSqlFunctionRepository : BaseRepository, IBaseSqlFunctionRepository
    {
    /// <summary>
    /// 
    /// </summary>
        public string FunctionType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="configuration"></param>
        public BaseSqlFunctionRepository(IDistributedCache cache, IConfiguration configuration) : base(cache, configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        /// <summary>
        /// Retrieves metadata for a given SQL function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A task representing the asynchronous operation, containing function metadata.</returns>
        public async Task<SqlFunctionMetadata> GetFunctionMetadataAsync(string functionName)
        {
            var functionDetail = await RetrieveFunctionDetailsAsync(functionName);
            var parameters = await FetchFunctionParametersWithDescriptionsAsync(functionName);
            var definition = await RetrieveFunctionDefinitionAsync(functionName) ?? string.Empty; // Ensure non-null assignment
            var dependencies = await FetchFunctionDependenciesAsync(functionName);

            return new SqlFunctionMetadata
            {
                FunctionDetail = functionDetail ?? new SqlFunctionDetail(), // Ensure non-null assignment
                Parameters = parameters,
                Definition = definition,
                Dependencies = dependencies,
                FunctionName = functionName
            };
        }

        /// <summary>
        /// Retrieves details of a given SQL function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A task representing the asynchronous operation, containing function details.</returns>
        private async Task<SqlFunctionDetail?> RetrieveFunctionDetailsAsync(string functionName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<SqlFunctionDetail>(
                    SqlQueryConstant.RetrieveFunctionDetails,
                    new { function_Type = FunctionType, function_name = functionName }
                );
            }
        }

        /// <summary>
        /// Fetches parameters with descriptions for a given SQL function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A task representing the asynchronous operation, containing function parameters.</returns>
        private async Task<IEnumerable<SqlFunctionParameter>> FetchFunctionParametersWithDescriptionsAsync(string functionName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<SqlFunctionParameter>(
                    SqlQueryConstant.FetchFunctionParametersWithDescriptions,
                    new { function_Type = FunctionType, function_name = functionName }
                )).ToList();
            }
        }

        /// <summary>
        /// Retrieves the definition of a given SQL function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A task representing the asynchronous operation, containing the function definition.</returns>
        private async Task<string?> RetrieveFunctionDefinitionAsync(string functionName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<string?>(
                    SqlQueryConstant.RetrieveFunctionDefinition,
                    new { function_Type = FunctionType, function_name = functionName }
                );
            }
        }

        /// <summary>
        /// Fetches dependencies of a given SQL function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>A task representing the asynchronous operation, containing function dependencies.</returns>
        private async Task<IEnumerable<SqlFunctionDependency>> FetchFunctionDependenciesAsync(string functionName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return (await connection.QueryAsync<SqlFunctionDependency>(
                    SqlQueryConstant.FetchFunctionDependencies,
                    new { function_name = functionName }
                )).ToList();
            }
        }

        /// <summary>
        /// Modifies the description of an existing SQL function.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The new description to be set.</param>
        private async Task ModifyFunctionDescriptionAsync(string schemaName, string functionName, string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(
                    SqlQueryConstant.ModifyFunctionDescription,
                    new { Schema_Name = schemaName, FunctionName = functionName, fun_value = description }
                );
            }
        }

        /// <summary>
        /// Adds a description to a new SQL function.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The description to be added.</param>
        private async Task AddFunctionDescriptionAsync(string schemaName, string functionName, string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(
                    SqlQueryConstant.AddFunctionDescription,
                    new { Schema_Name = schemaName, FunctionName = functionName, fun_value = description }
                );
            }
        }

        /// <summary>
        /// Checks if function metadata exists and updates it if found; otherwise, creates a new entry.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The function description.</param>
        public async Task UpsertFunctionDescriptionAsync(string schemaName, string functionName, string description)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var existingDescription = await connection.ExecuteScalarAsync<string>(
                    "SELECT value FROM sys.extended_properties WHERE major_id = OBJECT_ID(@FunctionName) AND name = 'MS_Description'",
                    new { FunctionName = functionName }
                );

                if (existingDescription != null)
                {
                    await ModifyFunctionDescriptionAsync(schemaName, functionName, description);
                }
                else
                {
                    await AddFunctionDescriptionAsync(schemaName, functionName, description);
                }
            }
        }

        /// <summary>
        /// Fetches descriptions of all scalar functions in the database.
        /// If a function appears multiple times, it merges descriptions.
        /// </summary>
        /// <returns>A dictionary of function names and their descriptions.</returns>
        public async Task<Dictionary<string, string>> FetchScalarFunctionDescriptionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<(string FunctionName, string Description)>(
                    SqlQueryConstant.FetchScalarFunctionDescriptions
                ).ConfigureAwait(false);

                return result
                    .GroupBy(x => x.FunctionName)  // Group by function name
                    .ToDictionary(
                        g => g.Key,  // Use function name as the key
                        g => string.Join(" | ", g.Select(x => x.Description ?? "No Description Available")) // Merge descriptions
                    );
            }
        }

        /// <summary>
        /// Fetches descriptions of all table functions in the database.
        /// If a function appears multiple times, it removes duplicates.
        /// </summary>
        /// <returns>A dictionary of function names and their descriptions.</returns>
        public async Task<Dictionary<string, string>> FetchTableFunctionDescriptionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<(string FunctionName, string Description)>(
                    SqlQueryConstant.FetchTableFunctionDescriptions
                ).ConfigureAwait(false);

                return result
                    .DistinctBy(x => x.FunctionName) // Removes duplicates (keeps first)
                    .ToDictionary(x => x.FunctionName, x => x.Description ?? "No Description Available");
            }
        }

        /// <summary>
        /// Fetches descriptions of all aggregate functions in the database.
        /// If a function appears multiple times, it removes duplicates.
        /// </summary>
        /// <returns>A dictionary of function names and their descriptions.</returns>
        public async Task<Dictionary<string, string>> FetchAggregateFunctionDescriptionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<(string FunctionName, string Description)>(
                    SqlQueryConstant.FetchAggregateFunctionDescriptions
                ).ConfigureAwait(false);

                return result
                    .DistinctBy(x => x.FunctionName) // Removes duplicates (keeps first)
                    .ToDictionary(x => x.FunctionName, x => x.Description ?? "No Description Available");
            }
        }
    }
}
