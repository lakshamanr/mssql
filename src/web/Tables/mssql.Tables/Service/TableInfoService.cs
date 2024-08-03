using Dapper;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using mssql.Tables.Common.Model.Tables;
using MSSQL.DIARY.COMN.Constant;
using mssql.server.Common;

namespace mssql.Tables.Service
{

    /// <summary>
    /// Service for retrieving information about database tables.
    /// </summary>
    public class TableInfoService
    {
        private readonly string _connectionString;
        private readonly ILogger<TableInfoService> _logger;

        /// <summary>
        /// Constructor for the TableInfoService.
        /// </summary>
        /// <param name="databaseSettings">Database settings injected via IOptions.</param>
        /// <param name="logger">Logger instance for logging information or errors.</param>
        public TableInfoService(IOptions<DatabaseSettings> databaseSettings, ILogger<TableInfoService> logger)
        {
            _connectionString = databaseSettings.Value.ConnectionString;
            _logger = logger;
        }

        /// <summary>
        /// Gets detailed information about a specific table.
        /// </summary>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>A <see cref="TableMetadata"/> containing detailed information about the table, or null if an error occurs.</returns>
        public async Task<TableMetadata> GetDetailedTableInfoAsync(string tableName)
        {
            try
            {
                var schemaAndTableName = tableName.Split('.');
                var schemaName = schemaAndTableName[0];
                var tableNameOnly = schemaAndTableName[1];

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var descriptions = await GetTableDescriptionAsync(db, schemaName, tableNameOnly);
                    var columns = await GetTableColumnInfoAsync(db, tableName);
                    var createScript = await GetTableCreateScriptAsync(db, tableName);
                    var indices = await GetTableIndexesAsync(db, tableName);
                    var foreignKeys = await GetTableForeignKeysAsync(db, tableName);
                    var properties = await GetDetailedTablePropertiesAsync(db, tableName);
                    var constraint = await GetTableTableConstraintAsync(db, tableName);
                    var detailedTableInfo = new TableMetadata
                    {
                        Descriptions = descriptions,
                        Columns = columns,
                        CreateScript = new TableCreateScript { Script = createScript },
                        Indices = indices,
                        ForeignKeys = foreignKeys,
                        Properties = properties,
                        Constraint = constraint
                    };

                    return detailedTableInfo;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting detailed table info for {TableName}", tableName);
                return null;
            }
        }

        /// <summary>
        /// Gets details of all tables.
        /// </summary>
        /// <returns>An enumerable of <see cref="TableProperty"/>.</returns>
        public async Task<IEnumerable<TableProperty>> GetTableDetailsAsync()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    return await db.QueryAsync<TableProperty>(SqlQueryConstant.GetAllTablesExtendedProperties);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table details");
                return new List<TableProperty>();
            }
        }

        /// <summary>
        /// Gets detailed properties of a specific table.
        /// </summary>
        /// <param name="db">Database connection.</param>
        /// <param name="tableName">The name of the table.</param>
        /// <returns>An enumerable of <see cref="TableProperty"/>.</returns>
        public async Task<IEnumerable<TableProperty>> GetDetailedTablePropertiesAsync(IDbConnection db, string tableName)
        {
            try
            {
                var schemaAndTableName = tableName.Split('.');
                var schemaName = schemaAndTableName[0];
                var tableNameOnly = schemaAndTableName[1];

                return await GetTablePropertiesAsync(db, schemaName, tableNameOnly);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting detailed table properties for {TableName}", tableName);
                return new List<TableProperty>();
            }
        }

        private async Task<IEnumerable<TableDescription>> GetTableDescriptionAsync(IDbConnection db, string schemaName, string tableName)
        {
            try
            {
                var query = SqlQueryConstant.GetAllExtendedPropertiesofTheTable
                    .Replace("@SchemaName", $"'{schemaName}'")
                    .Replace("@TableName", $"'{tableName}'");

                return await db.QueryAsync<TableDescription>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table description for {SchemaName}.{TableName}", schemaName, tableName);
                return new List<TableDescription>();
            }
        }

        private async Task<IEnumerable<TableProperty>> GetTablePropertiesAsync(IDbConnection db, string schemaName, string tableName)
        {
            try
            {
                var query = SqlQueryConstant.GetTableProperties
                    .Replace("@SchemaName", $"'{schemaName}'")
                    .Replace("@TableName", $"'{tableName}'");

                return await db.QueryAsync<TableProperty>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table properties for {SchemaName}.{TableName}", schemaName, tableName);
                return new List<TableProperty>();
            }
        }

        private async Task<IEnumerable<TableColumns>> GetTableColumnInfoAsync(IDbConnection db, string tableName)
        {
            try
            {
                return await db.QueryAsync<TableColumns>(SqlQueryConstant.GetAllTablesColumn, new { tblName = tableName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table column info for {TableName}", tableName);
                return new List<TableColumns>();
            }
        }

        private async Task<string> GetTableCreateScriptAsync(IDbConnection db, string tableName)
        {
            try
            {
                var query = SqlQueryConstant.GetTableCreateScript.Replace("@tableName", $"'{tableName}'");
                return await db.QueryFirstOrDefaultAsync<string>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table create script for {TableName}", tableName);
                return string.Empty;
            }
        }

        private async Task<IEnumerable<TableIndex>> GetTableIndexesAsync(IDbConnection db, string tableName)
        {
            try
            {
                return await db.QueryAsync<TableIndex>(SqlQueryConstant.GetTableIndex, new { tblName = tableName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table indexes for {TableName}", tableName);
                return new List<TableIndex>();
            }
        }

        private async Task<IEnumerable<TableForeignKey>> GetTableForeignKeysAsync(IDbConnection db, string tableName)
        {
            try
            {
                return await db.QueryAsync<TableForeignKey>(SqlQueryConstant.GetAllTableForeignKeys, new { tblName = tableName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table foreign keys for {TableName}", tableName);
                return new List<TableForeignKey>();
            }
        }

        private async Task<IEnumerable<TableConstraint>> GetTableTableConstraintAsync(IDbConnection db, string tableName)
        {
            try
            {
                return await db.QueryAsync<TableConstraint>(SqlQueryConstant.GetAllKeyConstraints, new { tblName = tableName });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting table constraints for {TableName}", tableName);
                return new List<TableConstraint>();
            }
        }

        /// <summary>
        /// Updates extended property for a specific table.
        /// </summary>
        /// <param name="tableDescription">The table description to update.</param>
        public async Task UpdateTableExtendedProperty(TableDescription tableDescription)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    var schemaAndTableName = tableDescription.Table.Split('.');
                    var schemaName = schemaAndTableName[0];
                    var tableNameOnly = schemaAndTableName[1];

                    var parameters = new DynamicParameters();
                    parameters.Add("@name", tableDescription.Name);
                    parameters.Add("@value", tableDescription.Value);
                    parameters.Add("@level0type", "SCHEMA");
                    parameters.Add("@level0name", schemaName);
                    parameters.Add("@level1type", "TABLE");
                    parameters.Add("@level1name", tableNameOnly);

                    await db.ExecuteAsync("sys.sp_updateextendedproperty", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating table extended property for {TableDescription}", tableDescription);
            }
        }
        public async Task UpdateTableColumnExtendedPropertyAsync(TableColumns tableColumns)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var schemaAndTableName = tableColumns.TableName.Split('.');
                var schemaName = schemaAndTableName[0];
                var tableName = schemaAndTableName[1];

                var parameters = new DynamicParameters();
                parameters.Add("@name", "MS_Description");
                parameters.Add("@value", tableColumns.Description);
                parameters.Add("@level0type", "SCHEMA");
                parameters.Add("@level0name", schemaName);
                parameters.Add("@level1type", "TABLE");
                parameters.Add("@level1name", tableName);
                parameters.Add("@level2type", "COLUMN");
                parameters.Add("@level2name", tableColumns.ColumnName);

                await db.ExecuteAsync("sys.sp_updateextendedproperty", parameters, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
