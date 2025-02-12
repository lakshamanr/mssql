using API.Domain.Common;

namespace API.Domain.Table
{
    /// <summary>
    /// Generates a hierarchical JSON structure from a list of strings.
    /// </summary>
    public class HirechyJsonGenerator
    {
        /// <summary>
        /// The root node of the hierarchy.
        /// </summary>
        public Node root;

        /// <summary>
        /// Initializes a new instance of the <see cref="HirechyJsonGenerator"/> class.
        /// </summary>
        /// <param name="l">The list of strings representing the hierarchy.</param>
        /// <param name="depancyName">The name of the root dependency.</param>
        /// <param name="referencesModels">Optional list of reference models.</param>
        public HirechyJsonGenerator(List<string> l, string depancyName, List<ReferencesModel> referencesModels = null)
        {
            root = new Node(depancyName) { ReferencesModels = referencesModels };

            foreach (string s in l)
            {
                addRow(s);
            }
        }

        /// <summary>
        /// Adds a row to the hierarchy.
        /// </summary>
        /// <param name="s">The string representing the row to add.</param>
        public void addRow(string s)
        {
            List<string> l = s.Split('/').ToList();
            Node state = root;
            foreach (string ss in l)
            {
                addSoon(state, ss);
                state = getSoon(state, ss);
            }
        }

        /// <summary>
        /// Adds a child node to the specified node if it does not already exist.
        /// </summary>
        /// <param name="n">The parent node.</param>
        /// <param name="s">The name of the child node to add.</param>
        private void addSoon(Node n, string s)
        {
            bool f = false;
            foreach (Node ns in n.Soon)
            {
                if (ns.name == s)
                {
                    f = !f;
                }
            }

            if (!f)
            {
                n.Soon.Add(new Node(s));
            }
        }

        /// <summary>
        /// Gets the child node with the specified name.
        /// </summary>
        /// <param name="n">The parent node.</param>
        /// <param name="s">The name of the child node to get.</param>
        /// <returns>The child node with the specified name, or null if it does not exist.</returns>
        private Node getSoon(Node n, string s)
        {
            foreach (Node ns in n.Soon)
            {
                if (ns.name == s)
                {
                    return ns;
                }
            }

            return null;
        }
    }
}
