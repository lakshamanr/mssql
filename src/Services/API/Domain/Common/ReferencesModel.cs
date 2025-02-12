namespace API.Domain.Common
{
    /// <summary>
    /// Represents a model for references with path, full entity name, type, and iteration.
    /// </summary>
    public class ReferencesModel
    {
        /// <summary>
        /// Gets or sets the path of the reference.
        /// </summary>
        public string ThePath { get; set; }

        /// <summary>
        /// Gets or sets the full entity name of the reference.
        /// </summary>
        public string TheFullEntityName { get; set; }

        /// <summary>
        /// Gets or sets the type of the reference.
        /// </summary>
        public string TheType { get; set; }

        /// <summary>
        /// Gets or sets the iteration of the reference.
        /// </summary>
        public int iteration { get; set; }
    }
}
