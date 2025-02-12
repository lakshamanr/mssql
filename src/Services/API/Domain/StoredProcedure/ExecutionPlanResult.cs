namespace API.Domain.StoredProcedure
{
    /// <summary>
    /// Represents the result of an execution plan.
    /// </summary>
    public class ExecutionPlanResult
    {
        /// <summary>
        /// Gets or sets the query plan.
        /// </summary>
        public string QueryPlan { get; set; }

        /// <summary>
        /// Gets or sets the use counts.
        /// </summary>
        public string UseCounts { get; set; }

        /// <summary>
        /// Gets or sets the cache object type.
        /// </summary>
        public string CacheObjectType { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes.
        /// </summary>
        public string SizeInBytes { get; set; }

        /// <summary>
        /// Gets or sets the SQL text.
        /// </summary>
        public string SQLText { get; set; }
    }

}
