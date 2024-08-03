namespace mssql.Tables.Common.Model.Tables
{
    public class TableMetadata
    {
        public IEnumerable<TableColumns> Columns { get; set; }
        public TableCreateScript CreateScript { get; set; }
        public IEnumerable<TableDescription> Descriptions { get; set; }
        public IEnumerable<TableIndex> Indices { get; set; }
        public IEnumerable<TableForeignKey> ForeignKeys { get; set; }
        public IEnumerable<TableProperty> Properties { get; set; }
        public IEnumerable<TableConstraint> Constraint { get; set; }
    }
    public class TableConstraint
    {
        public string table_view { get; set; }
        public string object_type { get; set; }
        public string constraint_type { get; set; }
        public string constraint_name { get; set; }
        public string details { get; set; }
    }
    public class TableCreateScript
    {
        public string Script { get; set; }
    }

    public class TableColumns
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string Key { get; set; }
        public string Identity { get; set; }
        public string DataType { get; set; }
        public string MaxLength { get; set; }
        public string AllowNulls { get; set; }
        public string Default { get; set; }
        public string Description { get; set; }
    }

    public class TableDescription
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Table { get; set; }
    }

    public class TableProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

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

    public class TableIndex
    {
        public string IndexName { get; set; }
        public string Columns { get; set; }
        public string IndexType { get; set; }
        public string IsUnique { get; set; }
        public string TableView { get; set; }
        public string ObjectType { get; set; }
    }
}
