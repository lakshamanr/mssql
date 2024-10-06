using API.Domain.LeftMenu;
using API.Factory.LeftMenu.Factory;
using API.Repository.Common;

namespace API.Service.LeftMenu.Service
{
    public class TreeViewJsonGenerator
    {
        private TreeViewConfiguration _treeViewContext;
        private readonly IBaseRepository _baseRepository;
        public TreeViewJsonGenerator(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
            _treeViewContext = PrepareTreeviewConfigurationApi();
        }

        private TreeViewConfiguration PrepareTreeviewConfigurationApi()
        {
            TreeViewConfiguration treeViewConfiguration = new TreeViewConfiguration();
            treeViewConfiguration.ProjectName = "Project";
            treeViewConfiguration.ServerName = _baseRepository.LoadDatabaseServerName();
            return treeViewConfiguration;
        }
        public async Task<TreeViewJson> GetProjectStructureAsync()
        {
            return await new TreeViewProjectNodeFactory(_treeViewContext, _baseRepository).CreateNodeAsync("");

        }
    }
}
