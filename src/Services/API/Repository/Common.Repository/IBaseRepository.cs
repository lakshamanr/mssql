using API.Domain.Database;
using API.Domain.Table;
using System.Data;

namespace API.Repository.Common.Repository
{
    public interface IBaseRepository
    {
        string LoadDatabaseServerName();
        string LoadDatabaseName();
        IDbConnection GetDbConnection();

        Task<IEnumerable<ServerProperty>> LoadAdvancedServerSettingsAsync(IDbConnection connection = null);
        Task<IEnumerable<DatabaseInfo>> LoadDatabases(IDbConnection connection = null);
        Task<DatabaseMetaData> GetDatabaseMetaData();
        Task<IEnumerable<FunctionInfo>> LoadScalarFunctionsAsync(string currentDbName = null);
        Task<IEnumerable<ServerProperty>?> LoadServerPropertiesAsync(IDbConnection connection = null);
        Task<IEnumerable<ProcedureInfo>> LoadStoredProceduresAsync(string currentDbName = null);
        Task<IEnumerable<FunctionInfo>> LoadTableValuedFunctionsAsync(string currentDbName = null);
        Task<IEnumerable<FunctionInfo>> LoadAggregateFunctionsAsync(string currentDbName = null);
        Task<IEnumerable<TriggerInfo>> LoadDatabaseTriggersAsync(string currentDbName = null);
        Task<IEnumerable<UserType>> LoadUserDefinedDataTypesAsync(string currentDbName = null);
        Task<IEnumerable<DbXmlSchema>> LoadXmlSchemaCollectionsAsync(string currentDbName = null);
        Task<IEnumerable<TablesMetadata>> LoadTablesAsync();
        Task<IEnumerable<TablesMetadata>> LoadTablesAsync(string currentDbName = null);
        Task<IEnumerable<ViewMetadata>> LoadViewAsync(string currentDbName = null);
    }
}