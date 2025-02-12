namespace API.Domain.Schemas
{
    /// <summary>
    /// Represents an object within a schema.
    /// </summary>
    public class SchemaObject
    {
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        public string ObjectType { get; set; }
    }
}
