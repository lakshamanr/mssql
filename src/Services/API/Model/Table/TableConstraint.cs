namespace API.Domain.Table
{
    public class TableConstraint
    {
        public string table_view { get; set; }
        public string object_type { get; set; }
        public string constraint_type { get; set; }
        public string constraint_name { get; set; }
        public string details { get; set; }
    }
}
