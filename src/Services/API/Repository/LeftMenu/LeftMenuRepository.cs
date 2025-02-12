
using Microsoft.Extensions.Caching.Distributed;
using API.Service.LeftMenu.Service;
using API.Domain.LeftMenu;
using Newtonsoft.Json;
using API.Repository.Common;
namespace API.Repository.LeftMenu
{

    /// <summary>
    /// Repository class for managing the left menu.
    /// </summary>
    public class LeftMenuRepository : BaseRepository, ILeftMenuRepository
    {
        private readonly ILogger<LeftMenuRepository> _logger;
        private TreeViewJsonGenerator TreeViewJsonGenerator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LeftMenuRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="logger">The logger instance.</param>
        /// <param name="cache">The distributed cache instance.</param>
        public LeftMenuRepository(ILogger<LeftMenuRepository> logger, IConfiguration configuration, IDistributedCache cache) : base(cache, configuration)
        {
            _logger = logger;
            TreeViewJsonGenerator = new TreeViewJsonGenerator(this);
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        /// <summary>
        /// Gets the left menu asynchronously.
        /// </summary>
        /// <returns>A JSON string representing the left menu.</returns>
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
                var leftMenuJson = await GenerateLeftMenuJsonAsync();

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
                // Log the exception
                _logger.LogError(ex, "An error occurred while retrieving the left menu data.");
                return JsonConvert.SerializeObject(new { error = "An error occurred while retrieving the left menu data." });
            }
        }

        /// <summary>
        /// Generates the left menu JSON asynchronously.
        /// </summary>
        /// <returns>A list of <see cref="TreeViewJson"/> representing the left menu.</returns>
        public async Task<List<TreeViewJson>> GenerateLeftMenuJsonAsync()
        {
            var data = new List<TreeViewJson>
                    {
                        await TreeViewJsonGenerator.GetProjectStructureAsync()
                    };
            return data;
        }
    }

}
