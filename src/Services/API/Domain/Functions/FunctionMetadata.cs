namespace API.Domain.Functions
{
    /// <summary>
    /// Represents metadata for a SQL function.
    /// </summary>
    public class SqlFunctionMetadata
    {
        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        /// Gets or sets the details of the function.
        /// </summary>
        public SqlFunctionDetail FunctionDetail { get; set; }

        /// <summary>
        /// Gets or sets the parameters of the function.
        /// </summary>
        public IEnumerable<SqlFunctionParameter> Parameters { get; set; }

        /// <summary>
        /// Gets or sets the definition of the function.
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// Gets or sets the dependencies of the function.
        /// </summary>
        public IEnumerable<SqlFunctionDependency> Dependencies { get; set; }
    }

}
