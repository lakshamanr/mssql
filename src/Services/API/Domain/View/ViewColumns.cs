namespace API.Domain.View
{
    /// <summary>
    /// Represents the columns of a view in the database.
    /// </summary>
    public class ViewColumns
    {
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        public string ViewName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the order of the column.
        /// </summary>
        public int ColumnOrder { get; set; }

        /// <summary>
        /// Gets or sets the data type of the column.
        /// </summary>
        public string DataType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the maximum length of the column.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets the precision of the column.
        /// </summary>
        public byte Precision { get; set; }

        /// <summary>
        /// Gets or sets the scale of the column.
        /// </summary>
        public byte Scale { get; set; }
    }
}
