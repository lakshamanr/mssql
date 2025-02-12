using API.Domain.Table;

namespace API.Repository.Table
{
    /// <summary>
    /// Interface for table repository operations.
    /// </summary>
    public interface ITableRepository
    {
        /// <summary>
        /// Loads the metadata for a specified table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the table metadata.</returns>
        Task<TableMetadata?> LoadTableMetadata(string tableName);

        /// <summary>
        /// Updates the extended property of a table column asynchronously.
        /// </summary>
        /// <param name="tableColumns">The table columns information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateTableColumnExtendedPropertyAsync(TableColumns tableColumns);

        /// <summary>
        /// Updates the extended property of a table.
        /// </summary>
        /// <param name="tableDescription">The table description information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateTableExtendedProperty(TableDescription tableDescription);
    }
}
