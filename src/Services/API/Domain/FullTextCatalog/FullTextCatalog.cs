namespace API.Domain.FullTextCatalog
{
    /// <summary>
    /// Represents a full-text catalog in the database.
    /// </summary>
    public class FullTextCatalog
    {
        /// <summary>
        /// Gets or sets the name of the full-text catalog.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the schema name of the full-text catalog.
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the owner of the full-text catalog.
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this catalog is the default catalog.
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this catalog is accent sensitive.
        /// </summary>
        public bool IsAccentSensitive { get; set; }

        /// <summary>
        /// Gets or sets the description of the full-text catalog.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the script to create the full-text catalog.
        /// </summary>
        public string CreateScript { get; set; }

        /// <summary>
        /// Gets or sets the scripts to create indexes for the full-text catalog.
        /// </summary>
        public IEnumerable<string> IndexScripts { get; set; }
    }
}
