using API.Domain.Table;
using API.Repository.Table;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TablesController : BaseController
    {
        private readonly TableRepository _tableRepository;
        private readonly TablesRepository _tablesRepository;
        public TablesController(TableRepository tableService, TablesRepository tablesService)
        {
            _tableRepository = tableService;
            _tablesRepository = tablesService;
        }

        [HttpGet("GetTableMetaData")]
        public async Task<TableMetadata> GetDetailedTableInfoAsync(string tableName)
        {
            return await _tableRepository.LoadTableMetadata(tableName);
        }

        [HttpGet("GetTableDetails")]
        public async Task<IEnumerable<TablesMetadata>> GetTableDetailsAsync()
        {
            return await _tablesRepository.LoadTablesAsync();
        }
        [HttpPost("UpdateTableExtendedProperties")]
        public async Task<IActionResult> UpdateTableExtendedProperties(TableDescription tableDescription)
        {
            await _tableRepository.UpdateTableExtendedProperty(tableDescription);
            return Ok();
        }
        [HttpPost("UpdateTableColumnExtendedProperty")]
        public async Task<IActionResult> UpdateTableColumnExtendedProperty(TableColumns tableColumns)
        {
            await _tableRepository.UpdateTableColumnExtendedPropertyAsync(tableColumns);
            return Ok();
        }
    }
}
