using API.Domain.View;

namespace API.Repository.View
{
    /// <summary>
    /// Interface for accessing view-related data.
    /// </summary>
    public interface IViewsRepository
    {
        /// <summary>
        /// Gets detailed information about views asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of view details.</returns>
        Task<IEnumerable<ViewDetails>> GetDetailedViewsInfoAsync();

        /// <summary>
        /// Gets view dependencies asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of view dependencies.</returns>
        Task<IEnumerable<ViewDependency>> GetViewDependenciesAsync();

        /// <summary>
        /// Gets metadata for a specific view asynchronously.
        /// </summary>
        /// <param name="viewName">The name of the view.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the view metadata.</returns>
        Task<ViewMetaData?> GetViewMetaDataAsync(string viewName);
    }
}
