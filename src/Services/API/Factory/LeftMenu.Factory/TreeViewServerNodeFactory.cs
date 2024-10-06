using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    public class TreeViewServerNodeFactory : TreeViewNodeFactory
    {

        public TreeViewServerNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository)
            : base(treeViewConfiguration, baseRepository)
        {

        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var databaseNodeFactory = new TreeViewDatabaseNodeFactory(_treeViewConfiguration, _baseRepository);
            var databaseNodes = await databaseNodeFactory.CreateNodeAsync("");
            return TreeViewNodeHelper.CreateTreeViewNode(
               text: _treeViewConfiguration.ServerName,
               icon: "fa fa-desktop fa-fw",
               link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}",
               schemaEnum: SchemaEnums.DatabaseServer,
               children: new List<TreeViewJson> { databaseNodes }
           );
        }

    }

}
