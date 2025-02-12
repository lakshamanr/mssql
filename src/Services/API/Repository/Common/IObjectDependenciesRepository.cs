
namespace API.Repository.Common
{
    /// <summary>
    /// Interface for object dependencies repository.
    /// </summary>
    public interface IObjectDependenciesRepository
    {
        /// <summary>
        /// Gets the dependencies of the specified object.
        /// </summary>
        /// <param name="ObjectName">The name of the object.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the dependencies of the object.</returns>
        Task<string> ObjectsDependencies(string ObjectName);
    }
}
