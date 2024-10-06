using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    public class TreeViewProjectNodeFactory : TreeViewNodeFactory
    {


        public TreeViewProjectNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository)
            : base(treeViewConfiguration, baseRepository)
        {

        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var serverNodeFactory = new TreeViewServerNodeFactory(_treeViewConfiguration, _baseRepository);
            var serverNode = await serverNodeFactory.CreateNodeAsync(currentDbName);

            return TreeViewNodeHelper.CreateTreeViewNode(
                text: _treeViewConfiguration.ProjectName,
                icon: "fa fa-home fa-fw",
                link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ProjectName}",
                schemaEnum: SchemaEnums.ProjectInfo,
                children: new List<TreeViewJson> { serverNode }
            );
        }
    }

}
