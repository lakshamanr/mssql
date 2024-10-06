export class ProcedureMetadata {
  // Add properties if necessary
}

export class CreateScript {
  public description: string; // Fixed spelling
}

export class Dependencies {
  public referencingObjectName: string; // Used camelCase for consistency
  public referencingObjectType: string;
  public referencedObjectName: string;
}

export class ExecutionPlan {
  public queryPlanXml: string; // Used proper camelCase for XML
  public useAccounts: string;  // Used camelCase for consistency
  public cacheObjectType: string;
  public sizeInBytes: string;  // Renamed to be more descriptive and accurate
  public sqlText: string;
}

export class Parameters {
  public id: number;
  public hideEdit: boolean; // Used camelCase
  public parameterName: string; // Used camelCase
  public type: string; // Simple name, more readable
  public length: string;
  public precision: string; // Used full word for clarity
  public scale: string;
  public parameterOrder: string; // More descriptive
  public extendedProperty: string; // Corrected spelling
}

export class ExtendedProperties {
  public description: string; // Fixed spelling
}
