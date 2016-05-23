using UnityTutorials.Models;

namespace UnityTutorials.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UnityTutorials.Models.WebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnityTutorials.Models.WebDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //context.Blogs.AddOrUpdate(
            //  p => p.Name,
            //  new Blog { Name = "Andrew Peters" },
            //  new Blog { Name = "Brice Lambson" },
            //  new Blog { Name = "Rowan Miller" }
            //);

        }
    }
}
