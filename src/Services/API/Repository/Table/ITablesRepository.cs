using API.Domain.Table;

namespace API.Repository.Table
{
    /// <summary>
    /// Interface for table repository to load table metadata.
    /// </summary>
    public interface ITablesRepository
    {
        /// <summary>
        /// Asynchronously loads the table metadata.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of table metadata.</returns>
        Task<IEnumerable<TablesMetadata>> LoadTablesAsync();
    }
}
