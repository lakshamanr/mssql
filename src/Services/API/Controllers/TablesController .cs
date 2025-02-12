using API.Domain.Table;
using API.Repository.Table;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
  /// <summary>
  /// Controller for handling table-related operations.
  /// </summary>
  [Route("[controller]")]
  [ApiController]
  public class TablesController : ControllerBase
  {
    private readonly ITableRepository _tableRepository;
    private readonly ITablesRepository _tablesRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="TablesController"/> class.
    /// </summary>
    /// <param name="tableService">The table repository service.</param>
    /// <param name="tablesService">The tables repository service.</param>
    public TablesController(ITableRepository tableService, ITablesRepository tablesService)
    {
      _tableRepository = tableService;
      _tablesRepository = tablesService;
    }

    /// <summary>
    /// Gets detailed metadata for a specific table.
    /// </summary>
    /// <param name="tableName">The name of the table.</param>
    /// <returns>The metadata of the table.</returns>
    [HttpGet("GetTableMetaData")]
    public async Task<ActionResult<TableMetadata>> GetDetailedTableInfoAsync(string tableName)
    {
      var result = await _tableRepository.LoadTableMetadata(tableName);
      if (result == null)
      {
        return NotFound();
      }
      return Ok(result);
    }

    /// <summary>
    /// Gets details of all tables.
    /// </summary>
    /// <returns>A list of table metadata.</returns>
    [HttpGet("GetTableDetails")]
    public async Task<IEnumerable<TablesMetadata>> GetTableDetailsAsync()
    {
      return await _tablesRepository.LoadTablesAsync();
    }

    /// <summary>
    /// Updates extended properties of a table.
    /// </summary>
    /// <param name="tableDescription">The table description containing extended properties.</param>
    /// <returns>An action result.</returns>
    [HttpPost("UpdateTableExtendedProperties")]
    public async Task<IActionResult> UpdateTableExtendedProperties(TableDescription tableDescription)
    {
      await _tableRepository.UpdateTableExtendedProperty(tableDescription);
      return Ok();
    }

    /// <summary>
    /// Updates extended properties of a table column.
    /// </summary>
    /// <param name="tableColumns">The table columns containing extended properties.</param>
    /// <returns>An action result.</returns>
    [HttpPost("UpdateTableColumnExtendedProperty")]
    public async Task<IActionResult> UpdateTableColumnExtendedProperty(TableColumns tableColumns)
    {
      await _tableRepository.UpdateTableColumnExtendedPropertyAsync(tableColumns);
      return Ok();
    }
  }
}
