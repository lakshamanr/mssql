using API.Domain.UserDefinedDataType;
using API.Repository.UserDefinedDataType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  
/// <summary>
    /// Controller for managing user-defined data types.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserDefinedDataTypeController : ControllerBase
    {
        private readonly IUserDefinedDataTypeRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDefinedDataTypeController"/> class.
        /// </summary>
        /// <param name="repository">The repository instance to interact with user-defined data types.</param>
        public UserDefinedDataTypeController(IUserDefinedDataTypeRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Get all user-defined data types.
        /// </summary>
        /// <returns>A list of all user-defined data types.</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDefinedDataType>>> GetAllUserDefinedDataTypes()
        {
            var dataTypes = await _repository.GetAllUserDefinedDataTypesAsync();
            return Ok(dataTypes);
        }

        /// <summary>
        /// Get details of a specific user-defined data type along with its extended properties.
        /// </summary>
        /// <param name="schemaName">The schema name of the user-defined data type.</param>
        /// <param name="typeName">The name of the user-defined data type.</param>
        /// <returns>The details of the specified user-defined data type.</returns>
        [HttpGet("details/{schemaName}/{typeName}")]
        public async Task<ActionResult<UserDefinedDataType>> GetUserDefinedDataTypeWithExtendedProperties(string schemaName, string typeName)
        {
            var dataType = await _repository.GetUserDefinedDataTypeWithExtendedPropertiesAsync(schemaName, typeName);

            if (dataType == null)
                return NotFound($"User-defined data type '{typeName}' not found in schema '{schemaName}'.");

            return Ok(dataType);
        }

        /// <summary>
        /// Add or update an extended property (description) for a user-defined data type.
        /// </summary>
        /// <param name="request">The request model containing schema name, type name, and description.</param>
        /// <returns>A status indicating the result of the operation.</returns>
        [HttpPost("upsert-extended-property")]
        public async Task<IActionResult> UpsertUserDefinedDataTypeExtendedProperty([FromBody] UpsertExtendedPropertyRequest request)
        {
            if (string.IsNullOrEmpty(request.SchemaName) || string.IsNullOrEmpty(request.TypeName))
                return BadRequest("SchemaName and TypeName are required.");

            await _repository.UpsertUserDefinedDataTypeExtendedPropertyAsync(request.SchemaName, request.TypeName, request.Description);
            return Ok($"Extended property for '{request.TypeName}' updated successfully.");
        }
    }
} 
