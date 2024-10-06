using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    internal class TreeViewSecurityNodeFactory : TreeViewNodeFactory
    {
        public TreeViewSecurityNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository) : base(treeViewConfiguration, baseRepository)
        {
        }

        public override Task<TreeViewJson> CreateNodeAsync(string currentDbName)
        {
            throw new NotImplementedException();
        }
    }
}
