namespace API.Domain.Table
{
    /// <summary>
    /// Represents the fragmentation details of a table.
    /// </summary>
    public class TableFragmentation
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the index.
        /// </summary>
        public string IndexName { get; set; }

        /// <summary>
        /// Gets or sets the percentage of fragmentation.
        /// </summary>
        public string PercentFragmented { get; set; }
    }
}
