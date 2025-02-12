namespace API.Domain.Table
{
    /// <summary>
    /// Represents an index on a table in the database.
    /// </summary>
    public class TableIndex
    {
        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// Gets or sets the columns included in the index.
        /// </summary>
        public string Columns { get; set; }

        /// <summary>
        /// Gets or sets the type of the index.
        /// </summary>
        public string IndexType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the index is unique.
        /// </summary>
        public string IsUnique { get; set; }

        /// <summary>
        /// Gets or sets the table or view the index is associated with.
        /// </summary>
        public string TableView { get; set; }

        /// <summary>
        /// Gets or sets the type of the object the index is associated with.
        /// </summary>
        public string ObjectType { get; set; }
    }
}
