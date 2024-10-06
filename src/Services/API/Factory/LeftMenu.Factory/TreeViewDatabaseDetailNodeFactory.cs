using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{

    public class TreeViewDatabaseDetailNodeFactory : TreeViewNodeFactory
    {
        public TreeViewDatabaseDetailNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }
        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var tablesTask = new TreeViewTableNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var viewsTask = new TreeViewViewNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var programmabilityTask = new TreeViewProgrammabilityNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);

            return TreeViewNodeHelper.CreateTreeViewNode(
                text: currentDbName,
                icon: "fa fa-folder",
                link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}",
                schemaEnum: SchemaEnums.AllDatabase,
                children: (await Task.WhenAll(tablesTask, viewsTask, programmabilityTask)).ToList()
            );
        }
    }
}
