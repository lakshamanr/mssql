namespace API.Domain.Table
{
    /// <summary>
    /// Represents a foreign key in a database table.
    /// </summary>
    public class TableForeignKey
    {
        /// <summary>
        /// Gets or sets the value of the foreign key.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the foreign key.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the schema name of the table containing the foreign key.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the table containing the foreign key.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the column containing the foreign key.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the name of the referenced table.
        /// </summary>
        public string ReferencedTable { get; set; }

        /// <summary>
        /// Gets or sets the name of the referenced column.
        /// </summary>
        public string ReferencedColumn { get; set; }
    }
}
