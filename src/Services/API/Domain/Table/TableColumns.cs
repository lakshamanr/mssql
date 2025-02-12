namespace API.Domain.Table
{
    /// <summary>
    /// Represents the columns of a table.
    /// </summary>
    public class TableColumns
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the key of the column.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the identity of the column.
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// Gets or sets the data type of the column.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the column.
        /// </summary>
        public string MaxLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether null values are allowed.
        /// </summary>
        public string AllowNulls { get; set; }

        /// <summary>
        /// Gets or sets the default value of the column.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Gets or sets the description of the column.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the extended property name of the column.
        /// </summary>
        public string ExtendedPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the extended property value of the column.
        /// </summary>
        public string ExtendedPropertyValue { get; set; }
    }
}
