using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    public class TreeViewTableNodeFactory : TreeViewNodeFactory
    {

        public TreeViewTableNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {

        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var tablesNode = TreeViewNodeHelper.CreateTreeViewNode(
                "Tables",
                "fa fa-folder",
                $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Tables",
                SchemaEnums.AllTable
            );
            var tables = await _baseRepository.LoadTablesAsync(currentDbName);
            if (tables.Any())
            {
                {

                    tablesNode.children = tables.Select(table =>
                        TreeViewNodeHelper.CreateTreeViewNode(
                            table.TableName,
                            "fa fa-table fa-fw",
                            $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Tables/{table.TableName}",
                            SchemaEnums.Table
                        )
                    ).ToList();
                }

            }
            return tablesNode;
        }
    }

}
