using API.Domain.Functions;
using API.Repository.Common;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Repository.Functions
{

  
/// <summary>
    /// Repository for handling scalar functions.
    /// </summary>
    public class ScalarFunctionRepository : BaseSqlFunctionRepository, IScalarFunctionRepository
    {
        private readonly IBaseSqlFunctionRepository _baseSqlFunctionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScalarFunctionRepository"/> class.
        /// </summary>
        /// <param name="iBaseSqlFunctionRepository">The base SQL function repository.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="cache">The distributed cache.</param>
        public ScalarFunctionRepository(IBaseSqlFunctionRepository iBaseSqlFunctionRepository, IConfiguration configuration, IDistributedCache cache)
            : base(cache, configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
            iBaseSqlFunctionRepository.FunctionType = "FN";
            _baseSqlFunctionRepository = iBaseSqlFunctionRepository;
        }

        /// <summary>
        /// Fetches the descriptions of scalar functions asynchronously.
        /// </summary>
        /// <returns>A dictionary containing function names and their descriptions.</returns>
        public new async Task<Dictionary<string, string>> FetchScalarFunctionDescriptionsAsync()
        {
            return await base.FetchScalarFunctionDescriptionsAsync();
        }

        /// <summary>
        /// Gets the metadata of a specific function asynchronously.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <returns>The metadata of the function.</returns>
        public new async Task<SqlFunctionMetadata> GetFunctionMetadataAsync(string functionName)
        {
            return await base.GetFunctionMetadataAsync(functionName);
        }

        /// <summary>
        /// Upserts the description of a function asynchronously.
        /// </summary>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="functionName">The function name.</param>
        /// <param name="description">The description of the function.</param>
        public new async Task UpsertFunctionDescriptionAsync(string schemaName, string functionName, string description)
        {
            await base.UpsertFunctionDescriptionAsync(schemaName, functionName, description);
        }
    }
}
