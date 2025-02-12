





namespace API.Repository.UserDefinedDataType
{
    /// <summary>
    /// Interface for User Defined Data Type Repository
    /// </summary>
    public interface IUserDefinedDataTypeRepository
    {
        /// <summary>
        /// Gets all user defined data types asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of user defined data types.</returns>
        Task<IEnumerable<Domain.UserDefinedDataType.UserDefinedDataType>> GetAllUserDefinedDataTypesAsync();

        /// <summary>
        /// Gets a user defined data type with extended properties asynchronously.
        /// </summary>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="typeName">The type name.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user defined data type with extended properties.</returns>
        Task<Domain.UserDefinedDataType.UserDefinedDataType?> GetUserDefinedDataTypeWithExtendedPropertiesAsync(string schemaName, string typeName);

        /// <summary>
        /// Upserts a user defined data type extended property asynchronously.
        /// </summary>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="typeName">The type name.</param>
        /// <param name="description">The description.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpsertUserDefinedDataTypeExtendedPropertyAsync(string schemaName, string typeName, string description);
    }
}
