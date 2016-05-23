using System;
using System.Data.Entity;

namespace UnityTutorials.Models
{
    public interface IWebDbContext : IDisposable
    {

    }

    public class WebDbContext : DbContext, IWebDbContext
    {
        public DbSet<Article> Articles { get; set; }

        public WebDbContext()
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Mappings
            modelBuilder.Configurations.Add(new ArticleMapping());

            base.OnModelCreating(modelBuilder);

        }

        public new void Dispose()
        {

        }
    }
}