using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewAggregateFunctionNodeFactory : TreeViewNodeFactory
    {
        public TreeViewAggregateFunctionNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var AggregateFunctionNode =
                            TreeViewNodeHelper.CreateTreeViewNode(
                           text: "Aggregate Functions",
                           icon: "fa fa-folder",
                           link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/AggregateFunctions",
                           schemaEnum: SchemaEnums.AllAggregateFunciton,
                           children: null
                       );

            var AggregateFunctions = await _baseRepository.LoadAggregateFunctionsAsync(currentDbName);

            if (AggregateFunctions.Any())
            {
                AggregateFunctionNode.children = AggregateFunctions
                   .Select(aggregate => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       aggregate.FunctionName,
                       "fa fa-cogs",
                       link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/AggregateFunctions{aggregate.FunctionName}",
                           SchemaEnums.AggregateFunciton
                   ))
                   .ToList();
            }
            return AggregateFunctionNode;
        }
    }
}
