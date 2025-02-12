namespace API.Domain.Table
{
    /// <summary>
    /// Represents a constraint on a table or view in the database.
    /// </summary>
    public class TableConstraint
    {
        /// <summary>
        /// Gets or sets the name of the table or view.
        /// </summary>
        public string table_view { get; set; }

        /// <summary>
        /// Gets or sets the type of the object (e.g., table, view).
        /// </summary>
        public string object_type { get; set; }

        /// <summary>
        /// Gets or sets the type of the constraint (e.g., primary key, foreign key).
        /// </summary>
        public string constraint_type { get; set; }

        /// <summary>
        /// Gets or sets the name of the constraint.
        /// </summary>
        public string constraint_name { get; set; }

        /// <summary>
        /// Gets or sets additional details about the constraint.
        /// </summary>
        public string details { get; set; }
    }
}
