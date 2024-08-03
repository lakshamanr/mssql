using Microsoft.AspNetCore.Mvc;

using mssql.Tables.Common.Model.Tables;
using mssql.Tables.Service;

namespace mssql.Tables.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly TableInfoService _tableInfoService;
        public TablesController(TableInfoService tableInfoService)
        {
            _tableInfoService = tableInfoService;
        }

        [HttpGet("GetTableMetaData")]
        public async Task<TableMetadata> GetDetailedTableInfoAsync(string tableName)
        {
            return await _tableInfoService.GetDetailedTableInfoAsync(tableName);
        }

        [HttpGet("GetTableDetails")]
        public async Task<IEnumerable<TableProperty>> GetTableDetailsAsync()
        {
            return await _tableInfoService.GetTableDetailsAsync();
        }
        [HttpPost("UpdateTableExtendedProperties")]
        public async Task<IActionResult> UpdateTableExtendedProperties(TableDescription tableDescription)
        {
            await _tableInfoService.UpdateTableExtendedProperty(tableDescription);
            return Ok();
        }
        [HttpPost("UpdateTableColumnExtendedProperty")]
        public async Task<IActionResult> UpdateTableColumnExtendedProperty(TableColumns tableColumns)
        {
            await _tableInfoService.UpdateTableColumnExtendedPropertyAsync(tableColumns);
            return Ok();
        }
    }
}
