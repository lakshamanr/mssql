
using API.Domain.LeftMenu;
using API.Repository.Common;
using API.Service.LeftMenu.Service;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
namespace API.Repository.LeftMenu
{

    public class LeftMenuRepository : BaseRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<LeftMenuRepository> _logger;
        private TreeViewJsonGenerator treeViewJsonGenerator { get; set; }

        public LeftMenuRepository(string connectionString, ILogger<LeftMenuRepository> logger, IDistributedCache cache)
            : base(connectionString, cache)
        {
            _connectionString = connectionString;
            _logger = logger;
            treeViewJsonGenerator = new TreeViewJsonGenerator(this);
        }

        public async Task<string> GetLeftMenuAsync()
        {
            try
            {
                // Try to get the cached left menu data
                var cachedMenuJson = await _cache.GetStringAsync("LeftMenuCacheKey");
                if (!string.IsNullOrEmpty(cachedMenuJson))
                {
                    return cachedMenuJson; // Return cached version
                }

                // Generate the left menu if not found in cache
                var leftMenuJson = await GetLeftMenuJsonAsync();

                // Check for null and handle accordingly
                if (leftMenuJson == null)
                {
                    return JsonConvert.SerializeObject(new { data = Array.Empty<TreeViewJson>() });
                }

                var serializedMenu = JsonConvert.SerializeObject(new { data = leftMenuJson });

                // Cache the left menu data for future requests
                await _cache.SetStringAsync("LeftMenuCacheKey", serializedMenu, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Cache for 10 minutes
                });

                return serializedMenu;
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                _logger.LogError(ex, "An error occurred while retrieving the left menu.");
                return JsonConvert.SerializeObject(new { error = "An error occurred while retrieving the left menu." });
            }
        }

        public async Task<List<TreeViewJson>> GetLeftMenuJsonAsync()
        {
            var data = new List<TreeViewJson>
        {
            await treeViewJsonGenerator.GetProjectStructureAsync()
        };
            return data;
        }
    }

}
