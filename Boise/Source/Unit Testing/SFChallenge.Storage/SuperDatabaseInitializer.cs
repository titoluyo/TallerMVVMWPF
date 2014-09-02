using System.Collections.Generic;
using System.Data.Entity;
using SFChallenge.Model;

namespace SFChallenge.Storage
{
    /// <summary>
    /// Initializes the database with seed data.
    /// </summary>
    public class SuperDatabaseInitializer : DropCreateDatabaseIfModelChanges<SuperDatabaseContext>
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(SuperDatabaseContext context)
        {
            base.Seed(context);

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
                 new SuperPerson() {
                     Id = 7,
                     Name = "Cheetah",
                     Allegiance = TeamNames.LegionOfDoom,
                     Rank = 3,
                     Strength = 3,
                     Health = 1000,
                     Resistance = 40,
                     Intellect = 70,
                     Speed = 95
                },
                new SuperPerson() {
                     Id = 3,
                     Name = "Batman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 2,
                     Strength = 6,
                     Health = 1000,
                     Resistance = 80,
                     Intellect = 70,
                     Speed = 40
                },  
                 new SuperPerson() {
                     Id = 8,
                     Name = "Riddler",
                     Allegiance = TeamNames.LegionOfDoom,
                     Rank = 2,
                     Strength = 4,
                     Health = 1000,
                     Resistance = 10,
                     Intellect = 90,
                     Speed = 60
                },
                new SuperPerson() {
                     Id = 4,
                     Name = "Green Lantern",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 4,
                     Strength = 5,
                     Health = 1000,
                     Resistance = 50,
                     Intellect = 50,
                     Speed = 50,
                },
                new SuperPerson() {
                     Id = 9,
                     Name = "Sinestro",
                     Allegiance = TeamNames.LegionOfDoom,
                     Rank = 4,
                     Strength = 5,
                     Health = 1000,
                     Resistance = 50,
                     Intellect = 50,
                     Speed = 50
                },
                new SuperPerson() {
                     Id = 5,
                     Name = "Aquaman",
                     Allegiance = TeamNames.SuperFriends,
                     Rank = 5,
                     Strength = 3,
                     Health = 1000,
                     Resistance = 10,
                     Intellect = 40,
                     Speed = 30,
                },                
                new SuperPerson() {
                     Id = 10,
                     Name = "Black Manta",
                     Allegiance = TeamNames.LegionOfDoom,
                     Rank = 5,
                     Strength = 4,
                     Health = 1000,
                     Resistance = 40,
                     Intellect = 10,
                     Speed = 20
                },
            };

            superPeople.ForEach(s => context.SuperPeople.Add(s));
            context.SaveChanges(); 
        }
    }
}
