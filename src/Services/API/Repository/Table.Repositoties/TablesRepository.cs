using Microsoft.Extensions.Caching.Distributed;
using API.Repository.Common.Repository;


namespace API.Repository.Table.Repositoties
{
    public class TablesRepository : BaseRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TableRepository> _logger;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// Constructor for the TableInfoService.
        /// </summary>
        /// <param name="databaseSettings">Database settings injected via IOptions.</param>
        /// <param name="logger">Logger instance for logging information or errors.</param>
        public TablesRepository(string connectionString, ILogger<TableRepository> logger, IDistributedCache cache) : base(connectionString, cache)
        {
            _connectionString = connectionString;
            _logger = logger;
            _cache = cache;

        }
    }
}
