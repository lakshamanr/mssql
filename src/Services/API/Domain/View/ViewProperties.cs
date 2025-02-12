namespace API.Domain.View
{
    /// <summary>
    /// Represents the properties of a view.
    /// </summary>
    public class ViewProperties
    {
        /// <summary>
        /// Gets or sets a value indicating whether ANSI nulls are used.
        /// </summary>
        public string uses_ansi_nulls { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether quoted identifier is used.
        /// </summary>
        public string uses_quoted_identifier { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the view.
        /// </summary>
        public string create_date { get; set; }

        /// <summary>
        /// Gets or sets the modification date of the view.
        /// </summary>
        public string modify_date { get; set; }
    }
}
