
using System;
using System.Data;
using System.Data.SqlServerCe;
using Ocean.Infrastructure;
using Ocean.Properties;

namespace Ocean.SqlCe {

    /// <summary>
    /// Represents DataAccess, providing classes to read from and write to the SQL Server CE database
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class DataAccess : IDataAccess {

        #region  Declarations

        readonly SqlCeConnection _sqlCeConnection;
        readonly String _connectionString;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the current SQL Connection.
        /// </summary>
        /// <returns>SQLConnection</returns>
        public SqlCeConnection SqlCeConnection {
            get {
                return _sqlCeConnection;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Creates a new instance of the DataAccess class utilizing the connection String passed as a parameter
        /// </summary>
        /// <param name="connectionString">SQL Client connection String</param>
        public DataAccess(String connectionString) {
            _connectionString = connectionString;
            _sqlCeConnection = new SqlCeConnection(_connectionString);
        }

        /// <summary>
        /// Creates a new instance of the DataAccess class utilizing the connection passed as a parameter
        /// </summary>
        /// <param name="sqlCeConnection">SQL Client Connection</param>
        public DataAccess(SqlCeConnection sqlCeConnection) {
            _sqlCeConnection = sqlCeConnection;
            _connectionString = sqlCeConnection.ConnectionString;
        }

        #endregion
        
        #region Private Helpers

        void LoadCommandParameters(SqlCeCommand cmd, params SqlCeParameter[] parameters) {
            if(parameters == null) return;
            foreach(SqlCeParameter p in parameters) {
                cmd.Parameters.Add(p);
            }
        }

        Boolean IsConnectionStateValid(SqlCeConnection cn) {
            return (cn.State == ConnectionState.Open || cn.State == ConnectionState.Closed);
        }
        
        #endregion //Private Helpers
        
        #region  ExecuteNonQuery

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
        public Int32 ExecuteNonQuery(String commandText, params SqlCeParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlCeConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "ExecuteNonQuery", String.Format(Resources.DataAccess_ExecuteNonQuery_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlCeConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "ExecuteNonQuery", Resources.DataAccess_ExecuteNonQuery_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(this.SqlCeConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                using(var cmd = new SqlCeCommand(commandText, this.SqlCeConnection)) {
                    LoadCommandParameters(cmd, parameters);
                    return cmd.ExecuteNonQuery();
                }

            }

            //connection will be controlled by this procedure, ie. connection state is closed
            using(this.SqlCeConnection) {
                using(var cmd = new SqlCeCommand(commandText, this.SqlCeConnection)) {
                    LoadCommandParameters(cmd, parameters);
                    this.SqlCeConnection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region  ExecuteScaler

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
        public Object ExecuteScaler(String commandText, params SqlCeParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlCeConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "ExecuteScaler", String.Format(Resources.DataAccess_ExecuteNonQuery_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlCeConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "ExecuteScaler", Resources.DataAccess_ExecuteNonQuery_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(this.SqlCeConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                using(var cmd = new SqlCeCommand(commandText, this.SqlCeConnection)) {
                    LoadCommandParameters(cmd, parameters);
                    return cmd.ExecuteScalar();
                }

            }

            //connection will be controlled by this procedure, ie. connection state is closed
            using(this.SqlCeConnection) {
                using(var cmd = new SqlCeCommand(commandText, this.SqlCeConnection)) {
                    LoadCommandParameters(cmd, parameters);
                    this.SqlCeConnection.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        #endregion

        #region  GetDataReader

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
        public SqlCeDataReader GetDataReader(String commandText, CommandBehavior commandBehavior, params SqlCeParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlCeConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "GetDataReader", String.Format(Resources.DataAccess_ExecuteNonQuery_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlCeConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "GetDataReader", Resources.DataAccess_ExecuteNonQuery_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            var cmd = new SqlCeCommand(commandText, this.SqlCeConnection);
            LoadCommandParameters(cmd, parameters);

            if(this.SqlCeConnection.State == ConnectionState.Closed) {
                this.SqlCeConnection.Open();
            }

            return cmd.ExecuteReader(commandBehavior);
        }

        #endregion
    }
}

