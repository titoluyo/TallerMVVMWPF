using System;
using SFChallenge.Model;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Provide database access to super people.
    /// </summary>
    public interface ISuperDatabaseContext
    {
        /// <summary>
        /// Gets the entity set of super people.
        /// </summary>
        IEntitySet<SuperPerson> SuperPeople { get; }

        /// <summary>
        /// Sets the super person's entity state to modified.
        /// </summary>
        /// <param name="superPerson">The super person set as modified.</param>
        void SetEntityStateModified(SuperPerson superPerson);

        /// <summary>
        /// Saves pending changes to the database.
        /// </summary>
        void SaveChanges();
    }
}
