using API.Domain.Triggers;
using API.Repository.Triggers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// API controller for managing database triggers.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class DatabaseTriggerController : ControllerBase
    {
        private readonly IDatabaseTriggerRepository _repository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
        public DatabaseTriggerController(IDatabaseTriggerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all database triggers.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllTriggers()
        {
            var triggers = await _repository.GetAllTriggersAsync();
            return Ok(triggers);
        }

        /// <summary>
        /// Retrieves details of a specific trigger by name.
        /// </summary>
        [HttpGet("{name}")]
        public async Task<IActionResult> GetTriggerByName(string name)
        {
            var trigger = await _repository.GetTriggerByNameAsync(name);
            if (trigger == null)
                return NotFound();
            return Ok(trigger);
        }
 

        /// <summary>
        /// Adds a new/update  extended property for a database trigger.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> MergeTriggerPropertyAsync([FromBody] DatabaseTrigger trigger)
        {
            var created = await _repository.MergeTriggerPropertyAsync(trigger.Name, trigger.Description);
            if (!created)
                return BadRequest("Creation failed/update failed");
            return Ok("Created successfully");
        }
    }
}
