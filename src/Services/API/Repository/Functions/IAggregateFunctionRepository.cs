using API.Domain.Functions;

namespace API.Repository.Functions
{
    /// <summary>
    /// Interface for repository handling aggregate functions.
    /// </summary>
    public interface IAggregateFunctionRepository
    {
        /// <summary>
        /// Fetches descriptions of aggregate functions.
        /// </summary>
        /// <returns>A dictionary with function names as keys and descriptions as values.</returns>
        Task<Dictionary<string, string>> FetchAggregateFunctionDescriptionsAsync();

        /// <summary>
        /// Gets metadata for a specific function.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>Metadata of the specified function.</returns>
        Task<SqlFunctionMetadata> GetFunctionMetadataAsync(string functionName);

        /// <summary>
        /// Upserts a description for a specific function.
        /// </summary>
        /// <param name="schemaName">The schema name of the function.</param>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="description">The description of the function.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpsertFunctionDescriptionAsync(string schemaName, string functionName, string description);
    }
}
