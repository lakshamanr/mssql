namespace API.Domain.Table
{
    /// <summary>
    /// Represents a property of a table.
    /// </summary>
    public class TableProperty
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        public string Value { get; set; }
    }
}
