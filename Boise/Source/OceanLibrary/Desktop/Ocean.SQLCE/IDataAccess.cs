using System;
using System.Data;
using System.Data.SqlServerCe;
using Ocean.Infrastructure;

namespace Ocean.SqlCe {
    /// <summary>
    /// Represents DataAccess, providing classes to read from and write to the SQL Server CE database
    /// </summary>
    public interface IDataAccess {
        /// <summary>
        /// Gets the current SQL Connection.
        /// </summary>
        /// <returns>SQLConnection</returns>
        SqlCeConnection SqlCeConnection { get; }

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns the number of rows affected
        /// </summary>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>The number of rows affected</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>
        /// <para>You can use the ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database without using a DataSet by executing UPDATE, INSERT, or DELETE statements</para>
        /// <para>Although the ExecuteNonQuery returns no rows, any output parameters or return values mapped to parameters are populated with data</para> 
        /// <para>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements, the return value is -1. If a rollback occurs, the return value is also -1</para> 
        /// </remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        Int32 ExecuteNonQuery(String commandText, params SqlCeParameter[] parameters);

        /// <summary>
        /// Executes a sql ExecuteScaler command using the supplied parameters and returns the value
        /// </summary>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>The number of rows affected</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        Object ExecuteScaler(String commandText, params SqlCeParameter[] parameters);

        /// <summary>
        /// Executes a sql command to return a SQL Client DataReader Object.  The developer MUST close the DataReader and Connection Object in the calling code when done with the reader.
        /// </summary>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">Command behaviors should be selected to get best performance.  The close connection behavior should be selected to ensure that the connection is closed when the DataReader is closed</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>SQLCeDataReader Object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        SqlCeDataReader GetDataReader(String commandText, CommandBehavior commandBehavior, params SqlCeParameter[] parameters);
    }
}