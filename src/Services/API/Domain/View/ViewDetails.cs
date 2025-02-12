namespace API.Domain.View
{
    /// <summary>
    /// Represents the details of a view.
    /// </summary>
    public class ViewDetails
    {
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the extended properties of the view.
        /// </summary>
        public string ViewExtendedProperties { get; set; }
    }
}
