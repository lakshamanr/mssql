namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents a parameter for a stored procedure.
    /// </summary>
    public class StoredProcedureParameter
    {
        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the length of the parameter.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the precision of the parameter.
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// Gets or sets the scale of the parameter.
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// Gets or sets the order of the parameter.
        /// </summary>
        public int ParameterOrder { get; set; }

        /// <summary>
        /// Gets or sets the collation of the parameter.
        /// </summary>
        public string Collation { get; set; }

        /// <summary>
        /// Gets or sets the extended property of the parameter.
        /// </summary>
        public string ExtendedProperty { get; set; }
    }

}
