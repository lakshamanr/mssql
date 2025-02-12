using API.Domain.Common;

namespace API.Domain.Table
{
  /// <summary>
  /// 
  /// </summary>
    public class Node
    {
        Dictionary<string, string> typeDescriptionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                        {
                                            { "AF", "(Aggregate function)" },
                                            { "C", "(CHECK constraint)" },
                                            { "D", "(DEFAULT)" },
                                            { "FN", "(SQL scalar function)" },
                                            { "FS", "(Assembly (CLR) scalar-function)" },
                                            { "FT", "(Assembly (CLR) table-valued function)" },
                                            { "IF", "(SQL inline table-valued function)" },
                                            { "IT", "(Internal table)" },
                                            { "P", "(SQL Stored Procedure)" },
                                            { "PC", "(Assembly (CLR) stored-procedure)" },
                                            { "PG", "(Plan guide)" },
                                            { "PK", "(PRIMARY KEY constraint)" },
                                            { "R", "(Rule (old-style, stand-alone))" },
                                            { "RF", "(Replication-filter-procedure)" },
                                            { "S", "(System base table)" },
                                            { "SN", "(Synonym)" },
                                            { "SO", "(Sequence object)" },
                                            { "U", "(Table - user-defined)" },
                                            { "V", "(View)" },
                                            { "EC", "(Edge constraint)" },
                                            { "SQ", "(Service queue)" },
                                            { "TA", "(Assembly (CLR) DML trigger)" },
                                            { "TF", "(SQL table-valued-function)" },
                                            { "TR", "(SQL DML trigger)" },
                                            { "TT", "(Table type)" },
                                            { "UQ", "(UNIQUE constraint)" },
                                            { "X", "(Extended stored procedure)" },
                                            { "XMLC", "(XML Data Type)" }
                                        };
    /// <summary>
    /// 
    /// </summary>
        public bool IblnFirstNode { get; set; }

    /// <summary>
    /// 
    /// </summary>
        public List<ReferencesModel> ReferencesModels { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Node(string n)
        {
            name = n;
            Soon = new List<Node>();
        }
    /// <summary>
    /// 
    /// </summary>
    public string name;
    /// <summary>
    /// 
    /// </summary>
    public string StyleClass { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Node> Soon;
    /// <summary>
    /// 
    /// </summary>
    public string PrimengToJson()
        {
            string s = "";
            if (IblnFirstNode)
            {
                s = s + "{" + "\"data\":[";
                IblnFirstNode = true;
            }
            else
            {
                s = s +
                    GetSQLObject(name);

            }
            bool f = true;
            foreach (Node n in Soon)
            {
                if (f) { f = !f; } else { s = s + ","; }
                s = s + n.PrimengToJson();
            }
            s = s + "]}";
            return s;
        }
    /// <summary>
    /// 
    /// </summary>
    private string GetSQLObject(string name)
        {

            // Extract the type description from the name
            string typeDescription = name.Substring(name.IndexOf('(') + 1).Replace(")", "").Trim();

            string iconClass = "";
            string styleClass = "TreeViewColor"; // Default style class if needed
                                                 // Determine the icon class based on the type description
            switch (typeDescription)
            {
                case "Aggregate function":
                    iconClass = "fa fa-cogs";
                    break;
                case "CHECK constraint":
                    iconClass = "fa fa-check";
                    break;
                case "DEFAULT":
                    iconClass = "fa fa-cogs";
                    break;
                case "SQL scalar function":
                    iconClass = "fa fa-cogs";
                    break;
                case "Assembly (CLR) scalar-function":
                    iconClass = "fa fa-cogs";
                    break;
                case "Assembly (CLR) table-valued function":
                    iconClass = "fa fa-cogs";
                    break;
                case "SQL inline table-valued function":
                    iconClass = "fa fa-cogs";
                    break;
                case "Internal table":
                    iconClass = "fa fa-database";
                    break;
                case "SQL Stored Procedure":
                    iconClass = "fa fa-cogs";
                    break;
                case "Assembly (CLR) stored-procedure":
                    iconClass = "fa fa-cogs";
                    break;
                case "Plan guide":
                    iconClass = "fa fa-cogs";
                    break;
                case "PRIMARY KEY constraint":
                    iconClass = "fa fa-key";
                    break;
                case "Rule (old-style, stand-alone)":
                    iconClass = "fa fa-cogs";
                    break;
                case "Replication-filter-procedure":
                    iconClass = "fa fa-cogs";
                    break;
                case "System base table":
                    iconClass = "fa fa-database";
                    break;
                case "Synonym":
                    iconClass = "fa fa-link";
                    break;
                case "Sequence object":
                    iconClass = "fa fa-sort-numeric-asc";
                    break;
                case "Table - user-defined":
                    iconClass = "fa fa-table";
                    break;
                case "View":
                    iconClass = "fa fa-eye";
                    break;
                case "Edge constraint":
                    iconClass = "fa fa-cogs";
                    break;
                case "Service queue":
                    iconClass = "fa fa-cogs";
                    break;
                case "Assembly (CLR) DML trigger":
                    iconClass = "fa fa-cogs";
                    break;
                case "SQL table-valued-function":
                    iconClass = "fa fa-cogs";
                    break;
                case "SQL DML trigger":
                    iconClass = "fa fa-cogs";
                    break;
                case "Table type":
                    iconClass = "fa fa-cogs";
                    break;
                case "UNIQUE constraint":
                    iconClass = "fa fa-cogs";
                    break;
                case "Extended stored procedure":
                    iconClass = "fa fa-cogs";
                    break;
                case "XML Data Type":
                    iconClass = "fa fa-file-code-o";
                    break;
                default:

                    break;
            }

            if (iconClass != null && iconClass != string.Empty)
            {

                return "{\"label\":\"" + name + "\","
                             + "\"expandedIcon\":\"" + iconClass + "\","
                             + "\"styleClass\":\"" + styleClass + "\","
                             + "\"collapsedIcon\":\"" + iconClass + "\","
                             + "\"children\":[";

            }

            return "{\"label\":\"" + name + "\","
                   + "\"expandedIcon\":\"fa fa-folder-open\","
                        + "\"styleClass\":\"" + styleClass + "\","
                        + "\"collapsedIcon\":\"fa fa-folder-close\","
                        + "\"children\":[";


        }
    /// <summary>
    /// 
    /// </summary>
    public string amexioToJson()
        {
            string s = "";
            if (IblnFirstNode)
            {
                s = s + "{\"data\":{\"" + "name" + "\":" + "\"" + name + "\"}" + ",\"data\":[";
                IblnFirstNode = true;
            }
            else
            {
                s = s
                        + "{\"text\":\"" + name + "\","
                        + "\"" + "icon" + "\"" + ":" + "\"" + "fa fa-folder-open" + "\"" + ","
                        + "\"" + "expand" + "\"" + ":" + "\"" + "true" + "\""
                        + ",\"children\":["
                    ;
            }
            bool f = true;
            foreach (Node n in Soon)
            {
                if (f) { f = !f; } else { s = s + ","; }
                s = s + n.amexioToJson();
            }
            s = s + "]}";
            return s;
        }
    }
}
