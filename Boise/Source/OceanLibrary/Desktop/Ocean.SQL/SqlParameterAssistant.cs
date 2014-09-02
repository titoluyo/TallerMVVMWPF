
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ocean.Sql {

    /// <summary>
    /// Represents SqlParameterAssistant, provides sql parameter builder methods.
    /// </summary>
    public static class SqlParameterAssistant {

        #region  Constructor

        /// <summary>
        /// Initializes the <see cref="SqlParameterAssistant"/> class.
        /// </summary>
        static SqlParameterAssistant() { }

        #endregion

        #region  Public Helper Routines

        /// <summary>
        /// If the value is null or a Nullable that does not have a value
        /// returns DBNull.Value otherwise, returns the value
        /// </summary>
        /// <param name="isNull">if set to <c>true</c> is null.</param>
        /// <param name="value">The value.</param>
        /// <returns>Non-null value or DBNull.Value</returns>
        public static Object Eval(Boolean isNull, Object value) {
            return isNull ? DBNull.Value : value;
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, Int32 size, Boolean nullable, ParameterDirection direction) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, Size = size, IsNullable = nullable };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, Int32 size, ParameterDirection direction) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, Size = size };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, Int32 size, ParameterDirection direction, Boolean nullable, Object value) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, Size = size, IsNullable = nullable, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="size">The size.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, Int32 size, ParameterDirection direction, Object value) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, Size = size, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="direction">The direction.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, ParameterDirection direction) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="nullable">if set to <c>true</c> [nullable].</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, ParameterDirection direction, Boolean nullable, Object value) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, IsNullable = nullable, Value = value };
        }

        /// <summary>
        /// Builds the SQL parameter.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="sqlDbType">Type of the SQL db.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameter(String parameterName, SqlDbType sqlDbType, ParameterDirection direction, Object value) {
            return new SqlParameter(parameterName, sqlDbType) { Direction = direction, Value = value };
        }


        /// <summary>
        /// Builds the SQL parameter return code.
        /// </summary>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameterReturnCode() {
            return BuildSqlParameterReturnCode(SqlConstants.STRING_RETURN_CODE);
        }

        /// <summary>
        /// Builds the SQL parameter return code.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameterReturnCode(String parameterName) {
            return new SqlParameter(parameterName, SqlDbType.Int) { Direction = ParameterDirection.Output };
        }

        /// <summary>
        /// Builds the SQL parameter return value.
        /// </summary>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameterReturnValue() {
            return BuildSqlParameterReturnValue(SqlConstants.STRING_RETURN_VALUE);
        }

        /// <summary>
        /// Builds the SQL parameter return value.
        /// </summary>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns></returns>
        public static SqlParameter BuildSqlParameterReturnValue(String parameterName) {
            return new SqlParameter(parameterName, SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
        }

        #endregion
    }
}