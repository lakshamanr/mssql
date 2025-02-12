namespace API.Domain.LeftMenu
{
    /// <summary>
    /// Represents the configuration for a tree view in the left menu.
    /// </summary>
    public class TreeViewConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewConfiguration"/> class.
        /// </summary>
        public TreeViewConfiguration()
        {
        }

        /// <summary>
        /// Gets or sets the list of database names.
        /// </summary>
        public List<string> DatabaseNames { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the server name.
        /// </summary>
        public string ServerName { get; set; }
    }
}
