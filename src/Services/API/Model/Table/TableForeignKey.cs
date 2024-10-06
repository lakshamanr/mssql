namespace API.Domain.Table
{
    public class TableForeignKey
    {
        public string Value { get; set; }
        public string Name { get; set; }
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ReferencedTable { get; set; }
        public string ReferencedColumn { get; set; }
    }
}
