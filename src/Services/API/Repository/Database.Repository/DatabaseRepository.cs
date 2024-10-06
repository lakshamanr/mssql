using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Repository.Database.Repository
{
    public class DatabaseRepository : BaseRepository
    {
        public DatabaseRepository(string connectionString, ILogger<DatabaseRepository> logger, IDistributedCache cache) : base(connectionString, cache)
        {
        }
    }
}
