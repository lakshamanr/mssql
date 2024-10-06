using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;


namespace API.Repository.Table
{
    public class TablesRepository : BaseRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<TableRepository> _logger;
        /// <summary>
        /// Constructor for the TableInfoService.
        /// </summary>
        /// <param name="databaseSettings">Database settings injected via IOptions.</param>
        /// <param name="logger">Logger instance for logging information or errors.</param>
        public TablesRepository(string connectionString, ILogger<TableRepository> logger, IDistributedCache cache) : base(connectionString, cache)
        {
            _connectionString = connectionString;
            _logger = logger;

        }
    }
}
