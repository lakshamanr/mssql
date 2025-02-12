using API.Domain.StoredProcedure;

namespace API.Repository.StoreProcedure
{
    /// <summary>
    /// Interface for stored procedure repository.
    /// </summary>
    public interface IStoredProcedureRepository
    {
        /// <summary>
        /// Gets all stored procedures asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of stored procedure information.</returns>
        Task<IEnumerable<StoredProcedureInfo>> GetAllStoredProceduresAsync();

        /// <summary>
        /// Gets the metadata of a stored procedure asynchronously.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the stored procedure metadata.</returns>
        Task<StoredProcedureMeta> GetStoredProcedureMetadataAsync(string storedProcedureName);

        /// <summary>
        /// Merges the description of a parameter asynchronously.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="description">The description of the parameter.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task MergeParameterDescriptionAsync(string schemaName, string storedProcedureName, string parameterName, string description);

        /// <summary>
        /// Merges the description of a stored procedure asynchronously.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <param name="description">The description of the stored procedure.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task MergeStoredProcedureDescriptionAsync(string schemaName, string storedProcedureName, string description);
    }
}
