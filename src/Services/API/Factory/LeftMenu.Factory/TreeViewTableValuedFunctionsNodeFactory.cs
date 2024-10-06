using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewTableValuedFunctionsNodeFactory : TreeViewNodeFactory
    {
        public TreeViewTableValuedFunctionsNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }
        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var tablevalueFunctionNode =
                           TreeViewNodeHelper.CreateTreeViewNode(
                          text: "Table-valued Functions",
                          icon: "fa fa-folder",
                          link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/TableValuedFunctions",
                          schemaEnum: SchemaEnums.AllStoreprocedure,
                          children: null
                      );

            var TableValuefunctions = await _baseRepository.LoadTableValuedFunctionsAsync(currentDbName);
            if (TableValuefunctions.Any())
            {
                tablevalueFunctionNode.children = TableValuefunctions
                   .Select(function => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       function.FunctionName,
                       "fa fa-cogs",
                       $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/TableValuedFunctions/{function.FunctionName}",
                       SchemaEnums.Storeprocedure
                   ))
                   .ToList();
            }
            return tablevalueFunctionNode;
        }
    }
}
