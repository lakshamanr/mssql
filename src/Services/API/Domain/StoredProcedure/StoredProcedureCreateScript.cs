namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents a script to create a stored procedure.
    /// </summary>
    public class StoredProcedureCreateScript
    {
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        public string StoredProcedureName { get; set; }

        /// <summary>
        /// Gets or sets the definition of the procedure.
        /// </summary>
        public string ProcedureDefinition { get; set; }
    }

}
