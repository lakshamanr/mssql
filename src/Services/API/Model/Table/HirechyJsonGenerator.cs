namespace API.Domain.Table
{
    public class HirechyJsonGenerator
    {
        public Node root;

        public HirechyJsonGenerator(List<string> l, string depancyName, List<ReferencesModel> referencesModels = null)
        {
            root = new Node(depancyName) { ReferencesModels = referencesModels };

            foreach (string s in l)
            {
                addRow(s);
            }
        }

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
