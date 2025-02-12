using API.Domain.Triggers;

namespace API.Repository.Triggers
{
    /// <summary>
    /// Interface for database trigger repository.
    /// </summary>
    public interface IDatabaseTriggerRepository
    {
        /// <summary>
        /// Gets all database triggers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of database triggers.</returns>
        Task<IEnumerable<DatabaseTrigger>> GetAllTriggersAsync();

        /// <summary>
        /// Gets a database trigger by name asynchronously.
        /// </summary>
        /// <param name="triggerName">The name of the trigger.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the database trigger if found; otherwise, null.</returns>
        Task<DatabaseTrigger?> GetTriggerByNameAsync(string triggerName);

        /// <summary>
        /// Merges the trigger property asynchronously.
        /// </summary>
        /// <param name="triggerName">The name of the trigger.</param>
        /// <param name="description">The description of the trigger.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean value indicating whether the merge was successful.</returns>
        Task<bool> MergeTriggerPropertyAsync(string triggerName, string description);
    }
}
