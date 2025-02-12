using API.Domain.Schemas;

namespace API.Repository.SchemaRepository
{
    /// <summary>
    /// Interface for schema repository operations.
    /// </summary>
    public interface ISchemaRepository
    {
        /// <summary>
        /// Gets all schema metadata asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of schema descriptions.</returns>
        Task<IEnumerable<SchemaDescription>> GetAllSchemaMetadataAsync();

        /// <summary>
        /// Gets the metadata of a specific schema asynchronously.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the schema metadata.</returns>
        Task<SchemaMetadata> GetSchemaMetadataAsync(string schemaName);
    }
}
