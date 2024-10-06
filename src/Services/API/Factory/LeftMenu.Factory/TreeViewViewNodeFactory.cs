using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewViewNodeFactory : TreeViewNodeFactory
    {
        public TreeViewViewNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {

        }
        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var viewNode = TreeViewNodeHelper.CreateTreeViewNode(
                              text: "Views",
                              icon: "fa fa-folder",
                              link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Views",
                              schemaEnum: SchemaEnums.AllViews,
                              children: null
                          );
            var viewDetails = await _baseRepository.LoadViewAsync(currentDbName);
            if (viewDetails.Any())
            {

                viewNode.children = viewDetails
                    .Select(view => TreeViewNodeHelper.CreateTreeViewNode(
                        view.ViewName,
                        "fa fa-eye",
                        $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Views/{view.ViewName}",
                        SchemaEnums.Views
                    ))
                    .ToList();
            }

            return viewNode;
        }
    }
}
