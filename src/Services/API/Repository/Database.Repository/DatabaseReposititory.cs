using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Repository.Database.Repository
{
    public class DatabaseReposititory : BaseRepository
    {
        public DatabaseReposititory(string connectionString, ILogger<DatabaseReposititory> logger, IDistributedCache cache) : base(connectionString, cache)
        {
        }
    }
}
