using SFChallenge.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using SFChallenge.Model;
using System.Collections.Generic;

namespace SFChallenge.Storage.UnitTests
{    
    // Note: Example of direct storage testing.

    [TestClass()]
    public class SuperDatabaseContextTest
    {
        [TestMethod()]
        public void WhenConstructed_ThenInitialized()
        {
            // Arrange            

            // Act
            SuperDatabaseContext actual = new SuperDatabaseContext();

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void WhenSuperPeopleAccessedWithEmptyData_ThenReturnsEmptyDbSet()
        {
            // Note: Example of TestDatabaseAssist drop & create.  There are tradeoffs here of correctness & portability vs. time.
            // Note: OK to have expected DB preconditions (e.g. lookup tables populated)

            // Arrange            
            TestDatabaseAssist.DropCreateDatabase();
            SuperDatabaseContext target = new SuperDatabaseContext();

            // Act
            var actual = target.SuperPeople.ToList();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        [TestMethod()]
        public void WhenSuperPeopleAccessedWithData_ThenReturnsPopulatedDbSet()
        {
            // Arrange           
            var superPeople = new List<SuperPerson>() {
                new SuperPerson() {
                     Id = 1,
                     Name = "Superman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 1,
                     Strength = 10,
                     Health = 1000,
                     Resistance = 80,
                     Intellect = 20,
                     Speed = 60
                },
                 new SuperPerson() {
                     Id = 6,                     
                     Name = "Lex Luthor",
                     Allegiance = TeamNames.LegionOfDoom,
                     Rank = 1,
                     Strength = 6,
                     Health = 1000,
                     Resistance = 40,
                     Intellect = 90,
                     Speed = 70
                },
                new SuperPerson() {
                     Id = 2,
                     Name = "Wonder Woman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 3,
                     Strength = 8,
                     Health = 1000,
                     Resistance = 60,
                     Intellect = 30,
                     Speed = 40,
                },
             };

            TestDatabaseAssist.DropCreateDatabase();

            // Note: Example of seeding as part of arranging a database test

            SuperDatabaseContext seedContext = new SuperDatabaseContext();

            foreach (var superPerson in superPeople)
            {
                seedContext.SuperPeople.Add(superPerson);
            }

            seedContext.SaveChanges();

            SuperDatabaseContext target = new SuperDatabaseContext();

            // Act
            var actual = target.SuperPeople.ToList();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
        }

        // Note: For EF we don't need much because we don't want to re-test EF, that's MSFT's job!
    }
}
