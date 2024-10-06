using API.Domain.Table;

namespace API.Domain.Database
{
    public class DatabaseMetaData
    {
        public IEnumerable<DbXmlSchema> DbXmlSchemas { get; set; }
        public string CurrentDatabaseName { get; set; }
        public IEnumerable<DatabaseInfo> DatabaseInfos { get; set; }
        public string DatabaseServerName { get; set; }
        public IEnumerable<ProcedureInfo> ProcedureInfos { get; set; }
        public IEnumerable<FunctionInfo> ScalarFunctionInfos { get; set; }
        public IEnumerable<ServerProperty> ServerAdvanceProperties { get; set; }
        public IEnumerable<ServerProperty> ServerProperties { get; set; }
        public IEnumerable<TriggerInfo> TriggerInfos { get; set; }
        public IEnumerable<FunctionInfo> TableFunctionInfos { get; set; }
        public IEnumerable<UserType> UserTypes { get; set; }
        public IEnumerable<DatabaseFile> fileInfomrations { get; set; }
        public IEnumerable<ViewMetadata> viewMetadata { get; set; }
        public IEnumerable<TablesMetadata> tablesMetadata { get; set; }
    }
}
