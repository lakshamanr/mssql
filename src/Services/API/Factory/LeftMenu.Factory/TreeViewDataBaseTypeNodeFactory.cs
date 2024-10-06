using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewDataBaseTypeNodeFactory : TreeViewNodeFactory
    {
        public TreeViewDataBaseTypeNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var UserDefinedDataType = new TreeViewUserDefinedDataTypeNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);
            var XmlSchemaCollection = new TreeViewXmlSchemaCollectionNodeFactory(_treeViewConfiguration, _baseRepository).CreateNodeAsync(currentDbName);

            return TreeViewNodeHelper.CreateTreeViewNode(
              text: "Type",
              icon: "fa fa-folder",
              link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/DataBaseType",
              schemaEnum: SchemaEnums.AllDatabaseDataTypes,
              children: (await Task.WhenAll(UserDefinedDataType, XmlSchemaCollection)).ToList()
          );
        }
    }
}
