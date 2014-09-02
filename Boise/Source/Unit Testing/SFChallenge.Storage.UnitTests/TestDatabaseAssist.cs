using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace SFChallenge.Storage.UnitTests
{
    public static class TestDatabaseAssist
    {
        public static void DropCreateDatabase()
        {
            SuperDatabaseContext context = GetDatabaseContext();
            DropAndCreateDatabase(context);
            context.SaveChanges();
        }
        
        private static SuperDatabaseContext GetDatabaseContext()
        {
            // This uses the configuration values in the app.config which point to a TestData.sdf file.
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");

            return new SuperDatabaseContext();
        }

        private static void DropAndCreateDatabase(SuperDatabaseContext context)
        {
            var replacedContext = ReplaceSqlCeConnection(context);

            if (replacedContext.Database.Exists())
            {
                replacedContext.Database.Delete();
            }
            context.Database.Create();
        }
        
        private static DbContext ReplaceSqlCeConnection(DbContext context)
        {
            if (context.Database.Connection is SqlCeConnection)
            {
                SqlCeConnectionStringBuilder builder =
                    new SqlCeConnectionStringBuilder(context.Database.Connection.ConnectionString);
                if (!String.IsNullOrWhiteSpace(builder.DataSource))
                {
                    builder.DataSource = ReplaceDataDirectory(builder.DataSource);
                    return new DbContext(builder.ConnectionString);
                }
            }
            return context;
        }

        private static string ReplaceDataDirectory(string inputString)
        {
            string str = inputString.Trim();
            if (string.IsNullOrEmpty(inputString) ||
                !inputString.StartsWith("|DataDirectory|", StringComparison.InvariantCultureIgnoreCase))
            {
                return str;
            }
            string data = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrEmpty(data))
            {
                data = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            }
            if (string.IsNullOrEmpty(data))
            {
                data = string.Empty;
            }
            int length = "|DataDirectory|".Length;
            if ((inputString.Length > "|DataDirectory|".Length) && ('\\' == inputString["|DataDirectory|".Length]))
            {
                length++;
            }
            return Path.Combine(data, inputString.Substring(length));
        }
    }
}
