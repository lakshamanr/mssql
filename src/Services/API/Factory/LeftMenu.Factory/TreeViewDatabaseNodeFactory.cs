using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    public class TreeViewDatabaseNodeFactory : TreeViewNodeFactory
    {
        public TreeViewDatabaseNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository)
            : base(treeViewConfiguration, baseRepository)
        {

        }
        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {

            var dbNode = TreeViewNodeHelper.CreateTreeViewNode(
                "User Database",
                "fa fa-folder",
                $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database",
                SchemaEnums.AllDatabase
            );

            var lstDatabaseInfo = await _baseRepository.LoadDatabases();

            dbNode.children = (await Task.WhenAll(lstDatabaseInfo.Select(async dbName =>
            {
                return await new TreeViewDatabaseDetailNodeFactory(_treeViewConfiguration, _baseRepository)
                            .CreateNodeAsync(dbName.Name);
            }))).ToList();

            return dbNode;
        }
    }

}
