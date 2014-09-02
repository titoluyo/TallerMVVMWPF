using System;
using Ocean.Sql.Properties;

namespace Ocean.Sql {

    /// <summary>
    /// Represents SqlConnectionStringAssistant
    /// </summary>
    internal static class SqlConnectionStringAssistant {

        /// <summary>
        /// Initializes the <see cref="SqlConnectionStringAssistant"/> class.
        /// </summary>
        static SqlConnectionStringAssistant() { }

        /// <summary>
        /// Parses the database name from connection string.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns></returns>
        public static String ParseDatabaseNameFromConnectionString(String connectionString) {

            String dataBaseName = String.Empty;
            String[] aryTemp = connectionString.Split(';');

            foreach(String s in aryTemp) {
                if (!s.ToLower().StartsWith("initial catalog") && !s.ToLower().StartsWith("database")) continue;
                String[] aryTemp2 = s.Split('=');
                dataBaseName = aryTemp2[1];
            }

            if(dataBaseName == string.Empty) {
                throw new Exception(String.Format(Resources.SqlConnectionStringAssistant_ParseDatabaseNameFromConnectionString_Could_not_parse_database_name_from_connection_string___0__FormatString, connectionString));
            }

            return dataBaseName;
        }
    }
}
