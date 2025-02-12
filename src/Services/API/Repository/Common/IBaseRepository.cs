using API.Domain.Database;
using API.Domain.Table;
using API.Domain.View;
using System.Data;

namespace API.Repository.Common
{
    /// <summary>
    /// Interface for base repository operations.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Loads the database server name.
        /// </summary>
        /// <returns>The database server name.</returns>
        string LoadDatabaseServerName();

        /// <summary>
        /// Gets the current database name.
        /// </summary>
        /// <returns>The current database name.</returns>
        string GetCurrentDatabaseName();

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        /// <returns>The database connection.</returns>
        IDbConnection GetDbConnection();

        /// <summary>
        /// Loads advanced server settings asynchronously.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the server properties.</returns>
        Task<IEnumerable<ServerProperty>> LoadAdvancedServerSettingsAsync(IDbConnection connection = null);

        /// <summary>
        /// Loads databases asynchronously.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the database information.</returns>
        Task<IEnumerable<DatabaseInfo>> LoadDatabases(IDbConnection connection = null);

        /// <summary>
        /// Loads scalar functions asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the function information.</returns>
        Task<IEnumerable<FunctionInfo>> LoadScalarFunctionsAsync(string currentDbName = null);

        /// <summary>
        /// Loads server properties asynchronously.
        /// </summary>
        /// <param name="connection">The database connection.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the server properties.</returns>
        Task<IEnumerable<ServerProperty>?> LoadServerPropertiesAsync(IDbConnection connection = null);

        /// <summary>
        /// Loads stored procedures asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the procedure information.</returns>
        Task<IEnumerable<ProcedureInfo>> LoadStoredProceduresAsync(string currentDbName = null);

        /// <summary>
        /// Loads table-valued functions asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the function information.</returns>
        Task<IEnumerable<FunctionInfo>> LoadTableValuedFunctionsAsync(string currentDbName = null);

        /// <summary>
        /// Loads aggregate functions asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the function information.</returns>
        Task<IEnumerable<FunctionInfo>> LoadAggregateFunctionsAsync(string currentDbName = null);

        /// <summary>
        /// Loads database triggers asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the trigger information.</returns>
        Task<IEnumerable<TriggerInfo>> LoadDatabaseTriggersAsync(string currentDbName = null);

        /// <summary>
        /// Loads user-defined data types asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user-defined data types.</returns>
        Task<IEnumerable<UserType>> LoadUserDefinedDataTypesAsync(string currentDbName = null);

        /// <summary>
        /// Loads XML schema collections asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the XML schema collections.</returns>
        Task<IEnumerable<DbXmlSchema>> LoadXmlSchemaCollectionsAsync(string currentDbName = null);

        /// <summary>
        /// Loads tables asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the tables metadata.</returns>
        Task<IEnumerable<TablesMetadata>> LoadTablesAsync(string currentDbName = null);

        /// <summary>
        /// Loads views asynchronously.
        /// </summary>
        /// <param name="currentDbName">The current database name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the view metadata.</returns>
        Task<IEnumerable<ViewMetadata>> LoadViewAsync(string currentDbName = null);

        /// <summary>
        /// Gets detailed views information asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the detailed views information.</returns>
        Task<IEnumerable<ViewDetails>> GetDetailedViewsInfoAsync();
    }
}
