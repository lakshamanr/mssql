using API.Domain.LeftMenu;

namespace API.Repository.LeftMenu
{
    /// <summary>
    /// Interface for Left Menu Repository
    /// </summary>
    public interface ILeftMenuRepository
    {
        /// <summary>
        /// Generates the left menu JSON asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of TreeViewJson.</returns>
        Task<List<TreeViewJson>> GenerateLeftMenuJsonAsync();

        /// <summary>
        /// Gets the left menu asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the left menu as a string.</returns>
        Task<string> GetLeftMenuAsync();
    }
}
