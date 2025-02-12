using API.Domain.Functions;

namespace API.Repository.Functions
{
    /// <summary>
    /// Interface for scalar function repository operations.
    /// </summary>
    public interface IScalarFunctionRepository
    {
        /// <summary>
        /// Fetches descriptions of aggregate functions.
        /// </summary>
        /// <returns>A dictionary with function names as keys and descriptions as values.</returns>
        Task<Dictionary<string, string>> FetchAggregateFunctionDescriptionsAsync();

        /// <summary>
        /// Fetches descriptions of scalar functions.
        /// </summary>
        /// <returns>A dictionary with function names as keys and descriptions as values.</returns>
        Task<Dictionary<string, string>> FetchScalarFunctionDescriptionsAsync();

        /// <summary>
        /// Fetches descriptions of table functions.
        /// </summary>
        /// <returns>A dictionary with function names as keys and descriptions as values.</returns>
        Task<Dictionary<string, string>> FetchTableFunctionDescriptionsAsync();

        /// <summary>
        /// Gets metadata for a specific function.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>The metadata of the specified function.</returns>
        Task<SqlFunctionMetadata> GetFunctionMetadataAsync(string functionName);

        /// <summary>
        /// Upserts a function description.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The description of the function.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpsertFunctionDescriptionAsync(string schemaName, string functionName, string description);
    }
}
