using Microsoft.Extensions.Caching.Distributed;
using API.Repository.Common;
using API.Domain.Table;
using System.Data.SqlClient;


namespace API.Repository.Table
{
    /// <summary>
    /// Repository class for handling table-related database operations.
    /// </summary>
    public class TablesRepository : BaseRepository, ITablesRepository
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor for the TableInfoService.
        /// </summary>
        /// <param name="databaseSettings">Database settings injected via IOptions.</param>
        /// <param name="logger">Logger instance for logging information or errors.</param>
        public TablesRepository(IConfiguration configuration, IDistributedCache cache) : base(cache, configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection") ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null");
        }

        /// <summary>
        /// Loads tables metadata from cache or queries the database.
        /// </summary>
        /// <returns>A collection of <see cref="TablesMetadata"/> instances.</returns>
        public async Task<IEnumerable<TablesMetadata>> LoadTablesAsync()
        {
            return await LoadTablesAsync(currentDbName: CurrentDatabases ?? "");
        }
    }
}
