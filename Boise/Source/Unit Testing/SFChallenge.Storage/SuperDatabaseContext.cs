using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using SFChallenge.Model;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Provide database access to super people through Entity Framework
    /// </summary>
    public class SuperDatabaseContext : DbContext, ISuperDatabaseContext
    {
        private EntitySet<SuperPerson> entitySet = new EntitySet<SuperPerson>();

        /// <summary>
        /// Gets the database entity set of super people.
        /// </summary>
        public DbSet<SuperPerson> SuperPeople { get; set; }

        /// <summary>
        /// Gets the entity set of super people.
        /// </summary>
        IEntitySet<SuperPerson> ISuperDatabaseContext.SuperPeople
        {
            get
            {
                // Note: Assumption is that generally the DbSet doesn't change.
                this.entitySet.UnderlyingDbSet = this.SuperPeople;
                return this.entitySet;
            }
        }

        /// <summary>
        /// Saves pending changes to the database.
        /// </summary>
        void ISuperDatabaseContext.SaveChanges()
        {
            this.SaveChanges();
        }

        /// <summary>
        /// Sets the super person's entity state to modified.
        /// </summary>
        /// <param name="superPerson">The super person set as modified.</param>
        void ISuperDatabaseContext.SetEntityStateModified(SuperPerson superPerson)
        {
            this.Entry(superPerson).State = System.Data.EntityState.Modified;
        }
    }
}
