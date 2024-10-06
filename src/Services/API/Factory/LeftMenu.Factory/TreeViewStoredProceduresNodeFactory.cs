using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewStoredProceduresNodeFactory : TreeViewNodeFactory
    {

        public TreeViewStoredProceduresNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {

        }
        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var StoredProceduresNode =
                            TreeViewNodeHelper.CreateTreeViewNode(
                           text: "StoredProcedures",
                           icon: "fa fa-folder",
                           link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/StoredProcedures",
                           schemaEnum: SchemaEnums.AllStoreprocedure,
                           children: null
                       );

            var databaseStroreProcedures = await _baseRepository.LoadStoredProceduresAsync(currentDbName);
            if (databaseStroreProcedures.Any())
            {
                StoredProceduresNode.children = databaseStroreProcedures
                   .Select(Procedure => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       Procedure.ProcedureName,
                       "fa fa-cogs",
                       $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Views/{Procedure.ProcedureName}",
                       SchemaEnums.Storeprocedure
                   ))
                   .ToList();
            }
            return StoredProceduresNode;
        }
    }
}
