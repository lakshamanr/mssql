namespace API.Domain.Table
{
    /// <summary>
    /// Represents metadata for a table.
    /// </summary>
    public class TablesMetadata
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the list of table columns.
        /// </summary>
        public List<TableColumns> tableColumns { get; set; }

        /// <summary>
        /// Gets or sets the name of the extended property.
        /// </summary>
        public string ExtendedPropertyName { get; set; }

        /// <summary>
        /// Gets or sets the value of the extended property.
        /// </summary>
        public string ExtendedPropertyValue { get; set; }
    }

}
