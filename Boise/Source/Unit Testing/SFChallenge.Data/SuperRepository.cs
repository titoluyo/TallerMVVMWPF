using System;
using System.Collections.Generic;
using System.Linq;
using SFChallenge.Model;
using SFChallenge.Storage;

namespace SFChallenge.Data
{
    /// <summary>
    /// Provides a repostitory of super people.
    /// </summary>
    public class SuperRepository : ISuperRepository
    {
        private ISuperDatabaseContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperRepository"/> class.
        /// </summary>
        /// <param name="context">The context of the database to use.</param>
        public SuperRepository(ISuperDatabaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        /// <summary>
        /// Gets the super person with the specified id.
        /// </summary>
        /// <param name="id">The id of the super person to get.</param>
        /// <returns>
        /// An ISuperPerson instance if found; otherwise null.
        /// </returns>
        /// <exception cref="System.ArgumentException">ID is less than 1.</exception>
        public ISuperPerson Get(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("ID is less than 1.", "id");
            }

            return this.context.SuperPeople.Find(id);   
        }

        /// <summary>
        /// Gets the entire collection of super people.
        /// </summary>
        /// <returns>
        /// An ISuperPerson collection.
        /// </returns>
        public IEnumerable<ISuperPerson> GetAll()
        {
            return this.context.SuperPeople.ToList();
        }

        /// <summary>
        /// Gets the collection of super people on the specified team.
        /// </summary>
        /// <param name="teamName">The team name to filter by.</param>
        /// <returns>
        /// An ISuperPerson collection.
        /// </returns>
        public IEnumerable<ISuperPerson> GetTeam(string teamName)
        {
            if (teamName == null)
            {
                throw new ArgumentNullException("teamName");
            }

            if (teamName.Length == 0)
            {
                throw new ArgumentException("Team name is empty.", "teamName");
            }

            return this.context.SuperPeople.Where(x => x.Allegiance == teamName).ToList();
        }

        /// <summary>
        /// Inserts the specified super person into the repository.
        /// </summary>
        /// <param name="superPerson">The super person to insert.</param>
        public void Insert(ISuperPerson superPerson)
        {
            if (superPerson == null)
            {
                throw new ArgumentNullException("superPerson");
            }

            this.context.SuperPeople.Add((SuperPerson)superPerson);
        }

        /// <summary>
        /// Updates the specified super person in the repository.
        /// </summary>
        /// <param name="superPerson">The super person to update.</param>
        public void Update(ISuperPerson superPerson)
        {
            if (superPerson == null)
            {
                throw new ArgumentNullException("superPerson");
            }

            this.context.SetEntityStateModified((SuperPerson)superPerson);
        }

        /// <summary>
        /// Deletes the specified super person from the repository.
        /// </summary>
        /// <param name="superPerson">The super person to delete.</param>
        public void Delete(ISuperPerson superPerson)
        {
            if (superPerson == null)
            {
                throw new ArgumentNullException("superPerson");
            }

            this.context.SuperPeople.Remove((SuperPerson)superPerson);
        }

        /// <summary>
        /// Saves pending changes to the repository.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }      
    }
}
