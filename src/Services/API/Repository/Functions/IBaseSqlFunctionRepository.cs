using API.Domain.Functions;

namespace API.Repository.Functions
{
    /// <summary>
    /// Interface for SQL function repository operations.
    /// </summary>
    public interface IBaseSqlFunctionRepository
    {
        /// <summary>
        /// Gets or sets the type of the function.
        /// </summary>
        string FunctionType { get; set; }

        /// <summary>
        /// Fetches descriptions of aggregate functions asynchronously.
        /// </summary>
        /// <returns>A dictionary containing function names and their descriptions.</returns>
        Task<Dictionary<string, string>> FetchAggregateFunctionDescriptionsAsync();

        /// <summary>
        /// Fetches descriptions of scalar functions asynchronously.
        /// </summary>
        /// <returns>A dictionary containing function names and their descriptions.</returns>
        Task<Dictionary<string, string>> FetchScalarFunctionDescriptionsAsync();

        /// <summary>
        /// Fetches descriptions of table functions asynchronously.
        /// </summary>
        /// <returns>A dictionary containing function names and their descriptions.</returns>
        Task<Dictionary<string, string>> FetchTableFunctionDescriptionsAsync();

        /// <summary>
        /// Gets metadata for a specific function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>The metadata of the specified function.</returns>
        Task<SqlFunctionMetadata> GetFunctionMetadataAsync(string functionName);

        /// <summary>
        /// Upserts a function description asynchronously.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The description of the function.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpsertFunctionDescriptionAsync(string schemaName, string functionName, string description);
    }
}
