namespace API.Domain.Schemas
{
    /// <summary>
    /// Represents the owner of a schema.
    /// </summary>
    public class SchemaOwner
    {
        /// <summary>
        /// Gets or sets the name of the schema.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the owner of the schema.
        /// </summary>
        public string Owner { get; set; }
    }

}
