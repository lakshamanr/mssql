namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents a request to describe a parameter of a stored procedure.
    /// </summary>
    public class ParameterDescriptionRequest
    {
        /// <summary>
        /// Gets or sets the schema name.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the stored procedure name.
        /// </summary>
        public string StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the parameter name.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the description of the parameter.
        /// </summary>
        public string Description { get; set; }
    }

}
