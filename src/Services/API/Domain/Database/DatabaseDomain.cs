namespace API.Domain.Database
{
    /// <summary>
    /// Represents information about a database.
    /// </summary>
    public class DatabaseInfo
    {
        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Represents a server property.
    /// </summary>
    public class ServerProperty
    {
        /// <summary>
        /// Gets or sets the name of the server property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the server property.
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// Represents information about a column.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; }
    }

    /// <summary>
    /// Represents information about a stored procedure.
    /// </summary>
    public class ProcedureInfo
    {
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        public string ProcedureName { get; set; }
    }

    /// <summary>
    /// Represents information about a function.
    /// </summary>
    public class FunctionInfo
    {
        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        public string FunctionName { get; set; }
    }

    /// <summary>
    /// Represents information about a trigger.
    /// </summary>
    public class TriggerInfo
    {
        /// <summary>
        /// Gets or sets the name of the trigger.
        /// </summary>
        public string TriggerName { get; set; }
    }

    /// <summary>
    /// Represents a user-defined type.
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// Gets or sets the name of the user-defined type.
        /// </summary>
        public string UserTypeName { get; set; }
    }

    /// <summary>
    /// Represents an XML schema in the database.
    /// </summary>
    public class DbXmlSchema
    {
        /// <summary>
        /// Gets or sets the name of the XML schema.
        /// </summary>
        public string SchemaName { get; set; }
    }
    /// <summary>
    /// Represents a database file.
    /// </summary>
    public class DatabaseFile
    {
        /// <summary>
        /// Gets or sets the name of the database file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the type of the database file.
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// Gets or sets the location of the database file.
        /// </summary>
        public string FileLocation { get; set; }

        /// <summary>
        /// Gets or sets the current size of the database file in megabytes.
        /// </summary>
        public decimal CurrentSizeMB { get; set; }

        /// <summary>
        /// Gets or sets the maximum size of the database file in megabytes.
        /// </summary>
        public string MaxSizeMB { get; set; }

        /// <summary>
        /// Gets or sets the growth type of the database file.
        /// </summary>
        public string GrowthType { get; set; }
    }
    /// <summary>
    /// Represents metadata for a database view.
    /// </summary>
    public class ViewMetadata
    {
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the extended property of the view.
        /// </summary>
        public string ExtendedProperty { get; set; }
    }
}
