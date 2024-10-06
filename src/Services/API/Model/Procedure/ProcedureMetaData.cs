namespace API.Model.Procedure
{
    public class ProcedureMetadata
    {
        public ExecutionPlan executionPlan;
    }

    public class CreateScript
    {
        public string Description { get; set; } // Fixed spelling
    }

    public class Dependencies
    {
        public string ReferencingObjectName { get; set; } // PascalCase for C#
        public string ReferencingObjectType { get; set; }
        public string ReferencedObjectName { get; set; }
    }

    public class ExecutionPlan
    {
        public string QueryPlanXml { get; set; } // PascalCase for XML
        public string UseAccounts { get; set; }
        public string CacheObjectType { get; set; }
        public string SizeInBytes { get; set; }  // More descriptive
        public string SqlText { get; set; }
    }

    public class Parameters
    {
        public int Id { get; set; }
        public bool HideEdit { get; set; } // Boolean type
        public string ParameterName { get; set; }
        public string Type { get; set; }
        public string Length { get; set; }
        public string Precision { get; set; } // Full word for clarity
        public string Scale { get; set; }
        public string ParameterOrder { get; set; } // More descriptive
        public string ExtendedProperty { get; set; } // Corrected spelling
    }

    public class ExtendedProperties
    {
        public string Description { get; set; } // Fixed spelling
    }

}
