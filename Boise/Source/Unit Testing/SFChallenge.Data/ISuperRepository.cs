using System.Collections.Generic;
using SFChallenge.Model;
namespace SFChallenge.Data
{
    /// <summary>
    /// Provides a repostitory of super people.
    /// </summary>
    public interface ISuperRepository
    {
        /// <summary>
        /// Gets the super person with the specified id.
        /// </summary>
        /// <param name="id">The id of the super person to get.</param>
        /// <returns>An ISuperPerson instance if found; otherwise null.</returns>
        /// <exception cref="System.ArgumentException">ID is less than 1.</exception>
        ISuperPerson Get(int id);

        /// <summary>
        /// Gets the entire collection of super people.
        /// </summary>
        /// <returns>An ISuperPerson collection.</returns>
        IEnumerable<ISuperPerson> GetAll();

        /// <summary>
        /// Gets the collection of super people on the specified team.
        /// </summary>
        /// <param name="teamName">The team name to filter by.</param>
        /// <returns>An ISuperPerson collection.</returns>
        IEnumerable<ISuperPerson> GetTeam(string teamName);

        /// <summary>
        /// Inserts the specified super person into the repository.
        /// </summary>
        /// <param name="superPerson">The super person to insert.</param>
        /// <remarks>
        /// No changes are made to the repository until SaveChanges is called.
        /// </remarks>
        void Insert(ISuperPerson superPerson);

        /// <summary>
        /// Updates the specified super person in the repository.
        /// </summary>
        /// <param name="superPerson">The super person to update.</param>
        /// <remarks>
        /// No changes are made to the repository until SaveChanges is called.
        /// </remarks>
        void Update(ISuperPerson superPerson);

        /// <summary>
        /// Deletes the specified super person from the repository.
        /// </summary>
        /// <param name="superPerson">The super person to delete.</param>
        /// <remarks>
        /// No changes are made to the repository until SaveChanges is called.
        /// </remarks>
        void Delete(ISuperPerson superPerson);

        /// <summary>
        /// Saves pending changes to the repository.
        /// </summary>
        void SaveChanges();
    }
}
