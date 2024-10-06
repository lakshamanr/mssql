using API.Domain.LeftMenu;
using API.Repository.Common;
namespace API.Factory.LeftMenu.Factory
{
    public abstract class TreeViewNodeFactory
    {
        public readonly IBaseRepository _baseRepository;
        public readonly TreeViewConfiguration _treeViewConfiguration;
        public TreeViewNodeFactory(TreeViewConfiguration treeViewConfiguration, IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
            _treeViewConfiguration = treeViewConfiguration;
        }
        public abstract Task<TreeViewJson> CreateNodeAsync(string currentDbName);
    }
}
