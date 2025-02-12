namespace API.Domain.UserDefinedDataType
{ 
  /// <summary>
    /// Request model for upserting an extended property.
    /// </summary>
    public class UpsertExtendedPropertyRequest
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
