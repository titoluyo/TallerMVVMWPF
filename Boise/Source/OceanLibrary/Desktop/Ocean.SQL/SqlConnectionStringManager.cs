using System;
using System.Collections.Generic;
using Ocean.Sql.Properties;

namespace Ocean.Sql {

    /// <summary>
    /// Represents SqlConnectionStringManager
    /// </summary>
    public static class SqlConnectionStringManager {

        #region  Declarations & Enums

        const string _STR_DEFAULT_CONNECTIONSTRING_KEY = "DEFAULT";

        static readonly Dictionary<String, String> _ConnectionStrings = new Dictionary<String, String>();

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes the <see cref="SqlConnectionStringManager"/> class.
        /// </summary>
        static SqlConnectionStringManager() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Using the default Key, looks up a previously stored connection string.
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown when the Key was not found in the collection.</exception>
        /// <returns>Database connection string</returns>
        public static String GetDefaultConnectionString() {
            if(_ConnectionStrings.ContainsKey(_STR_DEFAULT_CONNECTIONSTRING_KEY)) {
                return _ConnectionStrings[_STR_DEFAULT_CONNECTIONSTRING_KEY];
            }
            throw new KeyNotFoundException(String.Format(Resources.DataAccessConnection_GetConnectionString_The__0__was_not_found_in_the_collection_of_connection_strings_FormatString, _STR_DEFAULT_CONNECTIONSTRING_KEY));
        }

        /// <summary>
        /// Using a Key, looks up a previously stored connection string.
        /// </summary>
        /// <param name="connectionStringKey">String ConnectionStringKey is used to look up a previously stored connection string using the LoadConnectionString method.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the Key was not found in the collection.</exception>
        /// <returns>Database connection string</returns>
        public static string GetConnectionString(String connectionStringKey) {
            if(_ConnectionStrings.ContainsKey(connectionStringKey)) {
                return _ConnectionStrings[connectionStringKey];
            }
            throw new KeyNotFoundException(String.Format(Resources.DataAccessConnection_GetConnectionString_The__0__was_not_found_in_the_collection_of_connection_strings_FormatString, connectionStringKey));
        }

        /// <summary>
        /// Loads the connection string as the application default connection string
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public static void LoadDefaultConnectionString(String connectionString) {
            _ConnectionStrings[_STR_DEFAULT_CONNECTIONSTRING_KEY] = connectionString;
        }
        
        /// <summary>
        /// Loads a connection string 
        /// </summary>
        /// <param name="connectionStringKey">String ConnectionStringKey</param>
        /// <param name="connectionString">Connection string</param>
        public static void LoadConnectionString(String connectionStringKey, String connectionString) {
            _ConnectionStrings[connectionStringKey] = connectionString;
        }

        #endregion
    }
}