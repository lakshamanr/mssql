using API.Domain.LeftMenu;

namespace API.Common.Helper
{
    /// <summary>
    /// Provides helper methods for creating tree view nodes.
    /// </summary>
    public static class TreeViewNodeHelper
    {
        /// <summary>
        /// Creates a tree view node with the specified parameters.
        /// </summary>
        /// <param name="text">The text of the node.</param>
        /// <param name="icon">The icon of the node.</param>
        /// <param name="link">The link of the node.</param>
        /// <param name="schemaEnum">The schema enum of the node.</param>
        /// <param name="selected">Indicates whether the node is selected.</param>
        /// <param name="expand">Indicates whether the node is expanded.</param>
        /// <param name="badge">The badge number of the node.</param>
        /// <param name="children">The child nodes of the node.</param>
        /// <returns>A new instance of <see cref="TreeViewJson"/>.</returns>
        public static TreeViewJson CreateTreeViewNode(string text, string icon, string link, SchemaEnums schemaEnum, bool selected = false, bool expand = false, int badge = 0, List<TreeViewJson> children = null)
        {
            return new TreeViewJson
            {
                text = text,
                icon = icon,
                mdaIcon = text,
                link = link,
                selected = selected,
                expand = true,
                badge = badge,
                SchemaEnums = schemaEnum,
                children = children ?? new List<TreeViewJson>()
            };
        }
    }
}
