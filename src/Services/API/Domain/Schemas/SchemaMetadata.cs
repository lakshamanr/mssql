namespace API.Domain.Schemas
{
    /// <summary>
    /// Represents metadata for a schema.
    /// </summary>
    public class SchemaMetadata
    {
        /// <summary>
        /// Gets or sets the description of the schema.
        /// </summary>
        public SchemaDescription Description { get; set; }

        /// <summary>
        /// Gets or sets the owner of the schema.
        /// </summary>
        public SchemaOwner Owner { get; set; }

        /// <summary>
        /// Gets or sets the script associated with the schema.
        /// </summary>
        public string? Script { get; internal set; }

        /// <summary>
        /// Gets or sets the objects used by the schema asynchronously.
        /// </summary>
        public IEnumerable<SchemaObject> ObjectsUsedBySchemaAsync { get; set; }
    }
}
