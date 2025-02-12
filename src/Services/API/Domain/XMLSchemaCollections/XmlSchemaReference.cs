namespace API.Domain.XMLSchemaCollections
{
    /// <summary>
    /// Represents a reference to an XML schema collection.
    /// </summary>
    public class XmlSchemaReference
    {
        /// <summary>
        /// Gets or sets the schema of the table.
        /// </summary>
        public string TableSchema { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets the XML schema collection.
        /// </summary>
        public string XMLSchemaCollection { get; set; }
    }
}
