namespace API.Domain.UserDefinedDataType
{
    /// <summary>
    /// Represents a user-defined data type in the system.
    /// </summary>
    public class UserDefinedDataType
    {
        /// <summary>
        /// Gets or sets the full name (Schema + Type) of the user-defined data type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether nulls are allowed.
        /// </summary>
        public bool AllowNulls { get; set; }

        /// <summary>
        /// Gets or sets the base system type of the user-defined data type.
        /// </summary>
        public string BaseTypeName { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the user-defined data type.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the SQL script to recreate the user-defined data type.
        /// </summary>
        public string CreateScript { get; set; }

        /// <summary>
        /// Gets or sets the extended property (MS_Description) of the user-defined data type.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the references to other user-defined data types.
        /// </summary>
        public IEnumerable<UserDefinedDataTypeReference> userDefinedDataTypeReference { get; set; }
    }

}
