namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents information about a stored procedure.
    /// </summary>
    public class StoredProcedureInfo
    {
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        public string StoredProcedure { get; set; }

        /// <summary>
        /// Gets or sets the extended property of the stored procedure.
        /// </summary>
        public string ExtendedProperty { get; set; }
    }

}
