namespace API.Domain.StoredProcedure
{

    /// <summary>
    /// Represents metadata for a stored procedure, including dependencies, parameters, creation script, execution plan, and dependency tree.
    /// </summary>
    public class StoredProcedureMeta
    {
        /// <summary>
        /// Gets or sets the dependencies of the stored procedure.
        /// </summary>
        public IEnumerable<DependencyResult> Dependencies { get; set; } = new List<DependencyResult>();

        /// <summary>
        /// Gets or sets the parameters of the stored procedure.
        /// </summary>
        public IEnumerable<StoredProcedureParameter> Parameters { get; set; } = new List<StoredProcedureParameter>();

        /// <summary>
        /// Gets or sets the creation script of the stored procedure.
        /// </summary>
        public StoredProcedureCreateScript CreateScript { get; set; }

        /// <summary>
        /// Gets or sets the execution plan of the stored procedure.
        /// </summary>
        public ExecutionPlanResult ExecutionPlan { get; set; }

        /// <summary>
        /// Gets or sets the dependency tree of the stored procedure.
        /// </summary>
        public string StoredProcedureDependenciesTree { get; set; } = string.Empty;
    }

}
