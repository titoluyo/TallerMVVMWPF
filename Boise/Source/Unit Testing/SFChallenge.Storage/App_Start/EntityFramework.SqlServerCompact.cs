using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SFChallenge.Storage.App_Start {
    public static class EntityFramework_SqlServerCompact {
        public static void Start() {
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
            Database.SetInitializer<SuperDatabaseContext>(new SuperDatabaseInitializer());
        }
    }
}
