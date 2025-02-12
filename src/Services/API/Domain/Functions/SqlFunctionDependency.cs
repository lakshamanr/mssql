namespace API.Domain.Functions
{
    /// <summary>
    /// Represents a dependency of a SQL function.
    /// </summary>
    public class SqlFunctionDependency
    {
        /// <summary>
        /// Gets or sets the name of the dependency.
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the type of the dependency.
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the updated status of the dependency.
        /// </summary>
        public string Updated { get; set; }

        /// <summary>
        /// Gets or sets the selected status of the dependency.
        /// </summary>
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets the column name of the dependency.
        /// </summary>
        public string column_name { get; set; }
    }


}
