using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewScalarValuedFunctionNodeFactory : TreeViewNodeFactory
    {
        public TreeViewScalarValuedFunctionNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var ScalarfunctionNode =
                            TreeViewNodeHelper.CreateTreeViewNode(
                           text: "ScalarFunctions",
                           icon: "fa fa-folder",
                           link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/ScalarValuedFunctions",
                           schemaEnum: SchemaEnums.AllScalarValueFunctions,
                           children: null
                       );

            var scalarfunctions = await _baseRepository.LoadScalarFunctionsAsync(currentDbName);
            if (scalarfunctions.Any())
            {
                ScalarfunctionNode.children = scalarfunctions
                   .Select(scalarfunction => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       scalarfunction.FunctionName,
                       "fa fa-cogs",
                        link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/ScalarValuedFunctions/{scalarfunction.FunctionName}", SchemaEnums.ScalarValueFunctions
                   ))
                   .ToList();
            }
            return ScalarfunctionNode;
        }
    }
}
