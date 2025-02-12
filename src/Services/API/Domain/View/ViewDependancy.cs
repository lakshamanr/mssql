namespace API.Domain.View
{
    /// <summary>
    /// Represents a dependency in a view.
    /// </summary>
    public class ViewDependency
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
        public string updated { get; set; }

        /// <summary>
        /// Gets or sets the selected status of the dependency.
        /// </summary>
        public string selected { get; set; }

        /// <summary>
        /// Gets or sets the column name associated with the dependency.
        /// </summary>
        public string column_name { get; set; }

        /// <summary>
        /// Gets or sets the full reference name of the dependency.
        /// </summary>
        public string FullReferenceName { get; set; }
    }
}
