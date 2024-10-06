using API.Repository.Database.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseReposititory _repository;

        public DatabaseController(DatabaseReposititory repository)
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
