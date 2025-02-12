namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents the result of a dependency check between objects.
    /// </summary>
    public class DependencyResult
    {
        /// <summary>
        /// Gets or sets the name of the referencing object.
        /// </summary>
        public string ReferencingObjectName { get; set; }

        /// <summary>
        /// Gets or sets the type of the referencing object.
        /// </summary>
        public string ReferencingObjectType { get; set; }

        /// <summary>
        /// Gets or sets the name of the referenced object.
        /// </summary>
        public string ReferencedObjectName { get; set; }
    }

}
