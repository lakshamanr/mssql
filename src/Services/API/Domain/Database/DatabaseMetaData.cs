using API.Domain.Table;

namespace API.Domain.Database
{
    /// <summary>
    /// Represents metadata information about a database.
    /// </summary>
    public class DatabaseMetaData
    {
        /// <summary>
        /// Gets or sets the collection of XML schemas in the database.
        /// </summary>
        public IEnumerable<DbXmlSchema> DbXmlSchemas { get; set; }

        /// <summary>
        /// Gets or sets the name of the current database.
        /// </summary>
        public string CurrentDatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the collection of database information.
        /// </summary>
        public IEnumerable<DatabaseInfo> DatabaseInfos { get; set; }

        /// <summary>
        /// Gets or sets the name of the database server.
        /// </summary>
        public string DatabaseServerName { get; set; }

        /// <summary>
        /// Gets or sets the collection of stored procedure information.
        /// </summary>
        public IEnumerable<ProcedureInfo> ProcedureInfos { get; set; }

        /// <summary>
        /// Gets or sets the collection of scalar function information.
        /// </summary>
        public IEnumerable<FunctionInfo> ScalarFunctionInfos { get; set; }

        /// <summary>
        /// Gets or sets the collection of advanced server properties.
        /// </summary>
        public IEnumerable<ServerProperty> ServerAdvanceProperties { get; set; }

        /// <summary>
        /// Gets or sets the collection of server properties.
        /// </summary>
        public IEnumerable<ServerProperty> ServerProperties { get; set; }

        /// <summary>
        /// Gets or sets the collection of trigger information.
        /// </summary>
        public IEnumerable<TriggerInfo> TriggerInfos { get; set; }

        /// <summary>
        /// Gets or sets the collection of table function information.
        /// </summary>
        public IEnumerable<FunctionInfo> TableFunctionInfos { get; set; }

        /// <summary>
        /// Gets or sets the collection of user-defined types.
        /// </summary>
        public IEnumerable<UserType> UserTypes { get; set; }

        /// <summary>
        /// Gets or sets the collection of database file information.
        /// </summary>
        public IEnumerable<DatabaseFile> fileInformations { get; set; }

        /// <summary>
        /// Gets or sets the collection of view metadata.
        /// </summary>
        public IEnumerable<ViewMetadata> viewMetadata { get; set; }

        /// <summary>
        /// Gets or sets the collection of table metadata.
        /// </summary>
        public IEnumerable<TablesMetadata> tablesMetadata { get; set; }
    }
}
