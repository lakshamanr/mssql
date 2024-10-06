namespace API.Domain.Database
{
    public class DatabaseInfo
    {
        public string Name { get; set; }
    }

    public class ServerProperty
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class ColumnInfo
    {
        public string ColumnName { get; set; }
    }

    public class ProcedureInfo
    {
        public string ProcedureName { get; set; }
    }

    public class FunctionInfo
    {
        public string FunctionName { get; set; }
    }

    public class TriggerInfo
    {
        public string TriggerName { get; set; }
    }

    public class UserType
    {
        public string UserTypeName { get; set; }
    }

    public class DbXmlSchema
    {
        public string SchemaName { get; set; }
    }
    public class DatabaseFile
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileLocation { get; set; }
        public decimal CurrentSizeMB { get; set; }
        public string MaxSizeMB { get; set; }
        public string GrowthType { get; set; }
    }
    public class ViewMetadata
    {
        public string ViewName { get; set; }
        public string ExtendedProperty { get; set; }
    }
}
