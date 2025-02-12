namespace API.Domain.Table
{
    /// <summary>
    /// Represents metadata information about a database table.
    /// </summary>
    public class TableMetadata
    {
        /// <summary>
        /// Gets or sets the columns of the table.
        /// </summary>
        public IEnumerable<TableColumns> Columns { get; set; }

        /// <summary>
        /// Gets or sets the create script of the table.
        /// </summary>
        public TableCreateScript CreateScript { get; set; }

        /// <summary>
        /// Gets or sets the descriptions of the table.
        /// </summary>
        public IEnumerable<TableDescription> Descriptions { get; set; }

        /// <summary>
        /// Gets or sets the indices of the table.
        /// </summary>
        public IEnumerable<TableIndex> Indices { get; set; }

        /// <summary>
        /// Gets or sets the foreign keys of the table.
        /// </summary>
        public IEnumerable<TableForeignKey> ForeignKeys { get; set; }

        /// <summary>
        /// Gets or sets the properties of the table.
        /// </summary>
        public IEnumerable<TableProperty> Properties { get; set; }

        /// <summary>
        /// Gets or sets the constraints of the table.
        /// </summary>
        public IEnumerable<TableConstraint> Constraints { get; set; }

        /// <summary>
        /// Gets or sets the fragmentation information of the table.
        /// </summary>
        public IEnumerable<TableFragmentation> TableFragmentations { get; set; }

        /// <summary>
        /// Gets or sets the table dependencies tree.
        /// </summary>
        public string TableDependenciesTree { get; set; }
    }
}
