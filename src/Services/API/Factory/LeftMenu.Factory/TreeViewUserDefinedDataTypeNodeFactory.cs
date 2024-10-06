using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewUserDefinedDataTypeNodeFactory : TreeViewNodeFactory
    {
        public TreeViewUserDefinedDataTypeNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var UserDefinedNode =
                            TreeViewNodeHelper.CreateTreeViewNode(
                           text: "User-Defined Data Types",
                           icon: "fa fa-folder",
                           link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/DataBaseType/UserDefinedDataType",
                           schemaEnum: SchemaEnums.AllUserDefinedDataType,
                           children: null
                       );

            var UserDefined = await _baseRepository.LoadUserDefinedDataTypesAsync(currentDbName);

            if (UserDefined.Any())
            {
                UserDefinedNode.children = UserDefined
                   .Select(userDefined => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       userDefined.UserTypeName,
                       "fa fa-cogs",
                       link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/DataBaseType/UserDefinedDataType/{userDefined.UserTypeName}",
                       SchemaEnums.UserDefinedDataType
                   ))
                   .ToList();
            }
            return UserDefinedNode;
        }
    }
}
