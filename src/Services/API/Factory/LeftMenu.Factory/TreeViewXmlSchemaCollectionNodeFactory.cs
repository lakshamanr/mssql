using API.Common.Helper;
using API.Domain.LeftMenu;
using API.Repository.Common;

namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewXmlSchemaCollectionNodeFactory : TreeViewNodeFactory
    {
        public TreeViewXmlSchemaCollectionNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override async Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            var XmlSchemaNode =
                            TreeViewNodeHelper.CreateTreeViewNode(
                           text: "XML Schema Collections",
                           icon: "fa fa-folder",
                           link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/DataBaseType/XmlSchemaCollection",
                           schemaEnum: SchemaEnums.AllXMLSchemaCollection,
                           children: null
                       );

            var XmlSchemas = await _baseRepository.LoadXmlSchemaCollectionsAsync(currentDbName);

            if (XmlSchemas.Any())
            {
                XmlSchemaNode.children = XmlSchemas
                   .Select(XmlSchema => TreeViewNodeHelper.CreateTreeViewNode
                   (
                       XmlSchema.SchemaName,
                       "fa fa-cogs",
                       link: $"/{_treeViewConfiguration.ProjectName}/{_treeViewConfiguration.ServerName}/User Database/{currentDbName}/Programmability/DataBaseType/XmlSchemaCollection/{XmlSchema.SchemaName}",
                       SchemaEnums.XMLSchemaCollection
                   ))
                   .ToList();
            }
            return XmlSchemaNode;
        }
    }
}
