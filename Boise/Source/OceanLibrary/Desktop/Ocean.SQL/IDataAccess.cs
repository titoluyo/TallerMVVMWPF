using System;
using System.Data;
using System.Data.SqlClient;
using Ocean.Infrastructure;

namespace Ocean.Sql {
    /// <summary>
    /// Represents IDataAccess contract, providing methods to read from and write to the SQL Server database
    /// </summary>
    public interface IDataAccess {

        /// <summary>
        /// Gets the number of rows affected by the data operation.
        /// </summary>
        /// <returns>Integer</returns>
        Int32 AffectedRows { get; }

        /// <summary>
        /// Gets the ReturnCode from the data operation
        /// </summary>
        /// <returns>Integer</returns>
        DatabaseReturnCode ReturnCode { get; }

        /// <summary>
        /// Gets the current SQL Connection.
        /// </summary>
        /// <returns>SQLConnection</returns>
        SqlConnection SqlConnection { get; }

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns a single value.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <returns>A single value</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>Use the ExecuteScalar method to retrieve a single value (for example, an aggregate value) from a database. This requires less code than using the ExecuteReader method, and then performing the operations that you need to generate the single value using the data returned by a SqlDataReader.</remarks>
        Object ExecuteScaler(CommandType commandType, String commandText);

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns a single value.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>A single value</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>Use the ExecuteScalar method to retrieve a single value (for example, an aggregate value) from a database. This requires less code than using the ExecuteReader method, and then performing the operations that you need to generate the single value using the data returned by a SqlDataReader</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        Object ExecuteScaler(CommandType commandType, String commandText, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns a single value.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>A single value</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>Use the ExecuteScalar method to retrieve a single value (for example, an aggregate value) from a database. This requires less code than using the ExecuteReader method, and then performing the operations that you need to generate the single value using the data returned by a SqlDataReader.</remarks>
        Object ExecuteScaler(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns the number of rows affected
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <returns>The number of rows affected</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>
        /// <para>You can use the ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database without using a DataSet by executing UPDATE, INSERT, or DELETE statements</para>
        /// <para>Although the ExecuteNonQuery returns no rows, any output parameters or return values mapped to parameters are populated with data</para> 
        /// <para>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements, the return value is -1. If a rollback occurs, the return value is also -1</para> 
        /// </remarks>
        Int32 ExecuteNonQuery(CommandType commandType, String commandText);

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns the number of rows affected
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
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
        Int32 ExecuteNonQuery(CommandType commandType, String commandText, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Executes a sql command using the supplied parameters and returns the number of rows affected
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>The number of rows affected</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>
        /// <para>You can use the ExecuteNonQuery to perform catalog operations (for example, querying the structure of a database or creating database objects such as tables), or to change the data in a database without using a DataSet by executing UPDATE, INSERT, or DELETE statements</para>
        /// <para>Although the ExecuteNonQuery returns no rows, any output parameters or return values mapped to parameters are populated with data</para> 
        /// <para>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements, the return value is -1. If a rollback occurs, the return value is also -1</para> 
        /// </remarks>
        Int32 ExecuteNonQuery(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a dataset using the supplied parameters and table name(s).
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="tableNames">String array of table names that will be assigned to the returned dataset</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableNames" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataSet GetDataSet(CommandType commandType, String commandText, Int32 commandTimeOut, String[] tableNames, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a dataset using the supplied parameters and table name(s).
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="ds">Instianted dataset.  This can be a Typed dataset or an un-typed dataset</param>
        /// <param name="tableNames">String array of table names that will be assigned to the returned dataset</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="tableNames" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="ds" /><c>Is Nothing</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        DataSet GetDataSet(CommandType commandType, String commandText, Int32 commandTimeOut, DataSet ds, String[] tableNames, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a dataset using the supplied parameters and table name(s).
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="tableNames">String array of table names that will be assigned to the returned dataset</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableNames" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataSet GetDataSet(CommandType commandType, String commandText, String[] tableNames);

        /// <summary>
        /// Fills a dataset using the supplied parameters and table name(s).
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="tableNames">String array of table names that will be assigned to the returned dataset</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableNames" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataSet GetDataSet(CommandType commandType, String commandText, String[] tableNames, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a dataset using the supplied parameters and table name(s).
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="ds">Instianted dataset.  This can be a Typed dataset or an un-typed dataset</param>
        /// <param name="tableNames">String array of table names that will be assigned to the returned dataset</param>
        /// <param name="parameters">This is a param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableNames" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="ds" /><c>Is Nothing</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        DataSet GetDataSet(CommandType commandType, String commandText, DataSet ds, String[] tableNames, params SqlParameter[] parameters);

        /// <summary>
        /// Executes a sql command to return a SQL Client DataReader object.  The developer MUST close the DataReader and Connection object in the calling code when done with the reader.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">Command behaviors should be selected to get best performance.  The close connection behavior should be selected to ensure that the connection is closed when the DataReader is closed</param>
        /// <returns>SQL DataReader object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior);

        /// <summary>
        /// Executes a sql command to return a SQL Client DataReader object.  The developer MUST close the DataReader and Connection object in the calling code when done with the reader.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">Command behaviors should be selected to get best performance.  The close connection behavior should be selected to ensure that the connection is closed when the DataReader is closed</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>SQL DataReader object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Executes a sql command to return a SQL Client DataReader object.  The developer MUST close the DataReader and Connection object in the calling code when done with the reader.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">Command behaviors should be selected to get best performance.  The close connection behavior should be selected to ensure that the connection is closed when the DataReader is closed</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>SQL DataReader object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a datatable using the supplied parameters and table name.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="tableName">String table name that will be assigned to the returned datatable</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Datatable object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableName" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataTable GetDataTable(CommandType commandType, String commandText, Int32 commandTimeOut, String tableName, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a datatable using the supplied parameters and table name.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="dt">Instianted datatable</param>
        /// <param name="tableName">String table name that will be assigned to the returned datatable</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Dataset object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when Connection State not equal to Open or Closed</para>
        /// <para>Thrown when <paramref name="tableName" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="dt" /><c>Is Nothing</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        DataTable GetDataTable(CommandType commandType, String commandText, Int32 commandTimeOut, DataTable dt, String tableName, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a datatable using the supplied parameters and table name.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="tableName">String table name that will be assigned to the returned datatable</param>
        /// <returns>Datatable object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableName" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataTable GetDataTable(CommandType commandType, String commandText, String tableName);

        /// <summary>
        /// Fills a datatable using the supplied parameters and table name.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="tableName">String table name that will be assigned to the returned datatable</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Datatable object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableName" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        DataTable GetDataTable(CommandType commandType, String commandText, String tableName, params SqlParameter[] parameters);

        /// <summary>
        /// Fills a datatable using the supplied parameters and table name.
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="dt">Instianted datatable</param>
        /// <param name="tableName">String table name that will be assigned to the returned datatable</param>
        /// <param name="parameters">This is a param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Datatable object</returns>
        /// <exception cref="DataAccessException">
        /// <para>Thrown when <paramref name="tableName" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="commandText" /><c>Nothing or not assigned</c></para>
        /// <para>Thrown when <paramref name="dt" /><c>Is Nothing</c></para>
        /// </exception>
        /// <remarks>See the SQLDataAdapter class help documentation for a complete explaination of the Fill method.</remarks>
        DataTable GetDataTable(CommandType commandType, String commandText, DataTable dt, String tableName, params SqlParameter[] parameters);
    }
}