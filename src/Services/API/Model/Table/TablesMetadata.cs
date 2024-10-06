namespace API.Domain.Table
{
    public class TablesMetadata
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Level { get; set; }
        public string ColumnName { get; set; }
        public List<TableColumns> tableColumns { get; set; }
        public string ExtendedPropertyName { get; set; }
        public string ExtendedPropertyValue { get; set; }

    }

}
