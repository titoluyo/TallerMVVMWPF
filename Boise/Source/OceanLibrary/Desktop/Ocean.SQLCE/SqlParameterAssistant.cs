
using System;
using System.Data.SqlServerCe;

namespace Ocean.SqlCe {

    /// <summary>
    /// Represents SqlParameterAssistant, provides sql parameter builder methods.
    /// </summary>
    public static class SqlParameterAssistant {
        
        #region Constructor

        /// <summary>
        /// Initializes the <see cref="SqlParameterAssistant"/> class.
        /// </summary>
        static SqlParameterAssistant() { }

        #endregion //Constructor

        #region Methods

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlCeParameter BuildSqlParameter(String parameterName, System.Data.SqlDbType sqlDbType, Boolean nullable, Object value) {
            return new SqlCeParameter(parameterName, sqlDbType) { IsNullable = nullable, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <returns></returns>
        public static SqlCeParameter BuildSqlParameter(String parameterName, System.Data.SqlDbType sqlDbType, Int32 size, Boolean nullable) {
            return new SqlCeParameter(parameterName, sqlDbType) { Size = size, IsNullable = nullable };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlCeParameter BuildSqlParameter(String parameterName, System.Data.SqlDbType sqlDbType, Int32 size, Boolean nullable, Object value) {
            return new SqlCeParameter(parameterName, sqlDbType) { Size = size, IsNullable = nullable, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlCeParameter BuildSqlParameter(String parameterName, System.Data.SqlDbType sqlDbType, Int32 size, Object value) {
            return new SqlCeParameter(parameterName, sqlDbType) { Size = size, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlCeParameter BuildSqlParameter(String parameterName, System.Data.SqlDbType sqlDbType, Object value) {
            return new SqlCeParameter(parameterName, sqlDbType) { Value = value };
        }
        
        #endregion //Methods
    }
}