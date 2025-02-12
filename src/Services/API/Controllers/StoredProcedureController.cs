using API.Domain.StoredProcedure;
using API.Repository.StoreProcedure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Controller for handling stored procedure related operations.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class StoredProcedureController : ControllerBase
    {
        private readonly IStoredProcedureRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedureController"/> class.
        /// </summary>
        /// <param name="repository">The repository instance.</param>
        public StoredProcedureController(IStoredProcedureRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Gets all stored procedures.
        /// </summary>
        /// <returns>A list of stored procedures.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoredProcedureInfo>>> GetAllStoredProcedures()
        {
            var result = await _repository.GetAllStoredProceduresAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets the metadata of a specific stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure.</param>
        /// <returns>The metadata of the stored procedure.</returns>
        [HttpGet("{storedProcedureName}/metadata")]
        public async Task<ActionResult<StoredProcedureMeta>> GetStoredProcedureMetadata(string storedProcedureName)
        {
            var result = await _repository.GetStoredProcedureMetadataAsync(storedProcedureName);
            return Ok(result);
        }

        /// <summary>
        /// Merges the description of a stored procedure.
        /// </summary>
        /// <param name="request">The request containing the schema name, stored procedure name, and description.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost("description")]
        public async Task<IActionResult> MergeStoredProcedureDescription([FromBody] StoredProcedureDescriptionRequest request)
        {
            await _repository.MergeStoredProcedureDescriptionAsync(request.SchemaName, request.StoredProcedureName, request.Description);
            return Ok();
        }

        /// <summary>
        /// Merges the description of a stored procedure parameter.
        /// </summary>
        /// <param name="request">The request containing the schema name, stored procedure name, parameter name, and description.</param>
        /// <returns>An IActionResult.</returns>
        [HttpPost("parameter/description")]
        public async Task<IActionResult> MergeParameterDescription([FromBody] ParameterDescriptionRequest request)
        {
            await _repository.MergeParameterDescriptionAsync(request.SchemaName, request.StoredProcedureName, request.ParameterName, request.Description);
            return Ok();
        }
    }
}
