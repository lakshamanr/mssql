namespace API.Common
{
    /// <summary>
    /// Contains constants for caching database-related information.
    /// </summary>
    public static class CacheConstants
    {
        /// <summary>
        /// Contains cache keys for various database-related information.
        /// </summary>
        public static class DatabaseCache
        {
            /// <summary>
            /// Cache key for server metadata.
            /// </summary>
            public static string ServerMetaDataCacheKey = "ServerMetaDataCacheKey";

            /// <summary>
            /// Cache key for all database names.
            /// </summary>
            public static string DatabaseNames = "all_database_names";

            /// <summary>
            /// Cache key for stored procedures.
            /// </summary>
            public static string StoredProcedures = "stored_procedures";

            /// <summary>
            /// Cache key for database triggers.
            /// </summary>
            public static string DatabaseTriggers = "triggers";

            /// <summary>
            /// Cache key for scalar functions.
            /// </summary>
            public static string ScalarFunctions = "scalar_functions";

            /// <summary>
            /// Cache key for table-valued functions.
            /// </summary>
            public static string TableValuedFunctions = "table_valued_functions";

            /// <summary>
            /// Cache key for aggregate functions.
            /// </summary>
            public static string AggregateFunctions = "AggregateFunctions";

            /// <summary>
            /// Cache key for user-defined data types.
            /// </summary>
            public static string UserDefinedDataTypes = "user_defined_data_types";

            /// <summary>
            /// Cache key for XML schema collections.
            /// </summary>
            public static string XmlSchemaCollections = "xml_schema_collections";

            /// <summary>
            /// Cache key for server properties.
            /// </summary>
            public static string ServerProperties = "server_properties";

            /// <summary>
            /// Cache key for advanced server settings.
            /// </summary>
            public static string AdvancedServerSettings = "advanced_server_settings";

            /// <summary>
            /// Cache key for database tables.
            /// </summary>
            public static string DatabaseTables = "GetAllTableDetailsAsync";

            /// <summary>
            /// Cache key for database files.
            /// </summary>
            public static string DatabaseFiles = "DatabaseFiles";

            /// <summary>
            /// Cache key for view details.
            /// </summary>
            public static string ViewDetails = "ViewDetails";
        }
    }
}
