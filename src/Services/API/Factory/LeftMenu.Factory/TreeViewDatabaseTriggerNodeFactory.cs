using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewDatabaseTriggerNodeFactory : TreeViewNodeFactory
    {
        public TreeViewDatabaseTriggerNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var TriggerNode =
                             TreeViewNodeHelper.CreateTreeViewNode(
                            text: "Database Trigger",
                            icon: "fa fa-folder",
                            link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/Database Trigger",
                            schemaEnum: SchemaEnums.AllTriggers,
                            children: null
                        );

            var Triggers = await _baseRepository.LoadDatabaseTriggersAsync(currentDbName);
            if (Triggers.Any())
            {
                TriggerNode.children = Triggers
                   .Select(function => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       function.TriggerName,
                       "fa fa-cogs",
                       link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/Functions/Database Trigger{function.TriggerName}",
                       SchemaEnums.Triggers
                   ))
                   .ToList();
            }
            return TriggerNode;
        }
    }
}
