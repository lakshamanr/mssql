using API.Repository.SchemaRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for handling schema-related operations.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SchemaController : ControllerBase
    {
        private readonly ISchemaRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaController"/> class.
        /// </summary>
        /// <param name="repository">The schema repository.</param>
        public SchemaController(ISchemaRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all schema metadata asynchronously.
        /// </summary>
        /// <returns>A list of schema metadata.</returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAllSchemaMetadataAsync()
        {
            var metadata = await _repository.GetAllSchemaMetadataAsync();
            if (metadata == null)
                return NotFound();
            return Ok(metadata);
        }

        /// <summary>
        /// Gets the metadata for a specific schema asynchronously.
        /// </summary>
        /// <param name="schemaName">The name of the schema.</param>
        /// <returns>The metadata of the specified schema.</returns>
        [HttpGet("metadata/{schemaName}")]
        public async Task<IActionResult> GetSchemaMetadata(string schemaName)
        {
            var metadata = await _repository.GetSchemaMetadataAsync(schemaName);
            if (metadata == null)
                return NotFound();
            return Ok(metadata);
        }
    }
}
