





namespace API.Repository.FullTextCatalog
{
    /// <summary>
    /// Interface for Full Text Catalog Repository
    /// </summary>
    public interface IFullTextCatalogRepository
    {
        /// <summary>
        /// Gets all full text catalogs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of full text catalogs.</returns>
        Task<IEnumerable<Domain.FullTextCatalog.FullTextCatalog>> GetAllFullTextCatalogAsync();

        /// <summary>
        /// Gets a full text catalog by name asynchronously.
        /// </summary>
        /// <param name="catalogName">The name of the catalog.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the full text catalog if found; otherwise, null.</returns>
        Task<Domain.FullTextCatalog.FullTextCatalog?> GetFullTextCatalogByNameAsync(string catalogName);
    }
}
