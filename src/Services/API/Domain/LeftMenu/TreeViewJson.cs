namespace API.Domain.LeftMenu
{
    /// <summary>
    /// Represents a tree view item in the left menu.
    /// </summary>
    public class TreeViewJson
    {
        /// <summary>
        /// Gets or sets the text of the tree view item.
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Gets or sets the icon of the tree view item.
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// Gets or sets the MDA icon of the tree view item.
        /// </summary>
        public string mdaIcon { get; set; }

        /// <summary>
        /// Gets or sets the link of the tree view item.
        /// </summary>
        public string link { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tree view item is selected.
        /// </summary>
        public bool selected { get; set; }

        /// <summary>
        /// Gets or sets the badge number of the tree view item.
        /// </summary>
        public int badge { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tree view item is expanded.
        /// </summary>
        public bool expand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the tree view item is a leaf node.
        /// </summary>
        public bool leaf { get; set; }

        /// <summary>
        /// Gets or sets the schema enums of the tree view item.
        /// </summary>
        public SchemaEnums SchemaEnums { get; set; }

        /// <summary>
        /// Gets or sets the children of the tree view item.
        /// </summary>
        public IList<TreeViewJson> children { get; set; }
    }
}
