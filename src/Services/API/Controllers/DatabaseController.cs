using API.Repository.Database.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : BaseController
    {
        private readonly DatabaseRepository _repository;

        public DatabaseController(DatabaseRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("database-meta-data")]
        public async Task<IActionResult> GetDatabaseMetaDataAsync(CancellationToken cancellationToken)
        {
            var result = await _repository.GetDatabaseMetaData().ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(result);
        }


    }
}
