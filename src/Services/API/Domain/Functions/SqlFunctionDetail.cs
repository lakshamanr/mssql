namespace API.Domain.Functions
{
    /// <summary>
    /// Represents the details of a SQL function.
    /// </summary>
    public class SqlFunctionDetail
    {
        /// <summary>
        /// Gets or sets a value indicating whether ANSI_NULLS is used.
        /// </summary>
        public string uses_ansi_nulls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether QUOTED_IDENTIFIER is used.
        /// </summary>
        public string uses_quoted_identifier { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the SQL function.
        /// </summary>
        public string create_date { get; set; }

        /// <summary>
        /// Gets or sets the modification date of the SQL function.
        /// </summary>
        public string modify_date { get; set; }

        /// <summary>
        /// Gets or sets the name of the SQL function.
        /// </summary>
        public string name { get; set; }
    }
}
