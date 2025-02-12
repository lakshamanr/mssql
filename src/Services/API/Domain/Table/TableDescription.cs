namespace API.Domain.Table
{
    /// <summary>
    /// Represents a description of a table.
    /// </summary>
    public class TableDescription
    {
        /// <summary>
        /// Gets or sets the name of the table description.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the table description.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the table associated with the description.
        /// </summary>
        public string Table { get; set; }
    }
}
