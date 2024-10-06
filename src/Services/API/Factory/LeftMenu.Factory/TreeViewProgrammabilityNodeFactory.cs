using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewProgrammabilityNodeFactory : TreeViewNodeFactory
    {
        public TreeViewProgrammabilityNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {

        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var StoredProceduresNode = new TreeViewStoredProceduresNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var functionNode = new TreeViewFunctionNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var DatabaseTriggerNode = new TreeViewDatabaseTriggerNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var DataBaseTypeNode = new TreeViewDataBaseTypeNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);

            return TreeViewNodeHelper.CreateTreeViewNode(
              text: "Programmability",
              icon: "fa fa-folder",
              link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability",
              schemaEnum: SchemaEnums.AllDatabase,
              children: (await Task.WhenAll(StoredProceduresNode, functionNode, DatabaseTriggerNode, DataBaseTypeNode)).ToList()
          );
        }
    }
}
