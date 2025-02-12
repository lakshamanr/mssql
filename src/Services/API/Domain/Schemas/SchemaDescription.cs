namespace API.Domain.Schemas
{
    /// <summary>
    /// Represents a description of a schema.
    /// </summary>
    public class SchemaDescription
    {
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the description of the schema.
        /// </summary>
        public string Description { get; set; }
    }
}
