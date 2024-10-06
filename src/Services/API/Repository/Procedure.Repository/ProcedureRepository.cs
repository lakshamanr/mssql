using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Repository.Procedure.Repository
{
    public class ProcedureRepository : BaseRepository
    {
        readonly string _connectionString;
        readonly IDistributedCache _cache;
        readonly ILogger<ProcedureRepository> _logger;
        public ProcedureRepository(string connectionString, ILogger<ProcedureRepository> logger, IDistributedCache cache) : base(connectionString, cache)
        {
            _connectionString = connectionString;
            _cache = cache;
            _logger = logger;
        }
        public
    }
}
