namespace API.Domain.View
{
    /// <summary>
    /// Represents metadata for a view.
    /// </summary>
    public class ViewMetaData
    {
        /// <summary>
        /// Gets or sets the properties of the view.
        /// </summary>
        public IEnumerable<ViewProperties> ViewProperties { get; set; }

        /// <summary>
        /// Gets or sets the details of the view.
        /// </summary>
        public IEnumerable<ViewDetails> ViewDetails { get; set; }

        /// <summary>
        /// Gets or sets the dependencies of the view.
        /// </summary>
        public IEnumerable<ViewDependency> ViewDependencies { get; set; }

        /// <summary>
        /// Gets or sets the script to create the view.
        /// </summary>
        public ViewCreateScript ViewCreateScript { get; set; }

        /// <summary>
        /// Gets or sets the columns of the view.
        /// </summary>
        public IEnumerable<ViewColumns> ViewColumns { get; set; }
    }
}

