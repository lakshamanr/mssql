namespace API.Domain.UserDefinedDataType
{
    /// <summary>
    /// Represents a reference to a user-defined data type.
    /// </summary>
    public class UserDefinedDataTypeReference
    {
        /// <summary>
        /// Gets or sets the name of the object (Table/View/SP) that references the type.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Gets or sets the type of the object (Table, View, SP, etc.).
        /// </summary>
        public string ObjectType { get; set; }
    }
}
