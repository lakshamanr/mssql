namespace API.Domain.Table
{
    public class TableFragmentation
    {
        public string TableName { get; set; }
        public string IndexName { get; set; }
        public string PercentFragmented { get; set; }
    }
}
