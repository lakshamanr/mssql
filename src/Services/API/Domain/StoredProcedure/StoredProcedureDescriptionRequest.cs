namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents a request to describe a stored procedure.
    /// </summary>
    public class StoredProcedureDescriptionRequest
    {
        /// <summary>
        /// Gets or sets the schema name of the stored procedure.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        public string StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the description of the stored procedure.
        /// </summary>
        public string Description { get; set; }
    }
}
