using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewFunctionNodeFactory : TreeViewNodeFactory
    {
        public TreeViewFunctionNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var TableValueFunctionNode = new TreeViewTableValuedFunctionsNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var scalarValueFunctionNode = new TreeViewScalarValuedFunctionNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var AggregateFunctionNode = new TreeViewAggregateFunctionNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            return
             TreeViewNodeHelper.CreateTreeViewNode(
                             text: "Functions",
                             icon: "fa fa-folder",
                             link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions",
                             schemaEnum: SchemaEnums.AllFunctions,
                              children: (await Task.WhenAll(TableValueFunctionNode, scalarValueFunctionNode, AggregateFunctionNode)).ToList()
                        );
        }


    }
}
