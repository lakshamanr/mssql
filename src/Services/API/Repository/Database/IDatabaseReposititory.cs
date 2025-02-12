using API.Domain.Database;

namespace API.Repository.Database
{
    /// <summary>
    /// Interface for database repository operations.
    /// </summary>
    public interface IDatabaseReposititory
    {
        /// <summary>
        /// Gets the metadata of the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the database metadata.</returns>
        Task<DatabaseMetaData> GetDatabaseMetaData();
    }
}
