using System;
using System.Data;
using System.Data.SqlClient;
using Ocean.Infrastructure;
using Ocean.Sql.Properties;

namespace Ocean.Sql {

    /// <summary>
    /// Represents DataAccess, providing methods to read from and write to the SQL Server database
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class DataAccess : IDataAccess {

        #region  Declarations

        DatabaseReturnCode _returnCode = DatabaseReturnCode.NotSet;
        readonly String _connectionString;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the number of rows affected by the data operation.
        /// </summary>
        /// <returns>Integer</returns>
        public Int32 AffectedRows { get; private set; }

        /// <summary>
        /// Gets the ReturnCode from the data operation
        /// </summary>
        /// <returns>DatabaseReturnCode</returns>
        public DatabaseReturnCode ReturnCode {
            get {
                return _returnCode;
            }
        }

        /// <summary>
        /// Gets the current SQL Connection.
        /// </summary>
        /// <returns>SQLConnection</returns>
        public SqlConnection SqlConnection { get; private set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Creates a new instance of the DataAccess class utilizing the connection String passed as a parameter
        /// </summary>
        /// <param name="connectionString">SQL Client connection String</param>
        public DataAccess(String connectionString) {
            this.AffectedRows = -1;
            _connectionString = connectionString;
            this.SqlConnection = new SqlConnection(_connectionString);
        }

        /// <summary>
        /// Creates a new instance of the DataAccess class utilizing the connection passed as a parameter
        /// </summary>
        /// <param name="sqlConnection">SQL Client Connection</param>
        public DataAccess(SqlConnection sqlConnection) {
            this.AffectedRows = -1;
            _connectionString = sqlConnection.ConnectionString;
            this.SqlConnection = sqlConnection;
        }

        #endregion

        #region  Private Helper Routines

        Boolean IsConnectionStateValid(SqlConnection cn) {
            if (cn == null) {
                this.SqlConnection = new SqlConnection(_connectionString);
                return true;
            }
            return (cn.State == ConnectionState.Open || cn.State == ConnectionState.Closed);
        }

        DataAccessException BuildDataAccessException(DatabaseReturnCode returnCode, String source, String message) {

            if(message.Length == 0) {

                switch(returnCode) {

                    case DatabaseReturnCode.OperationFailed:
                        message = Resources.DataAccess_BuildDataAccessException_Operation_failed___Please_contact_tech_support_;

                        break;
                    case DatabaseReturnCode.RecordAlreadyInDatabase:
                        message = Resources.DataAccess_BuildDataAccessException_Record_is_already_in_the_database_;

                        break;
                    case DatabaseReturnCode.RecordNotFound:
                        message = Resources.DataAccess_BuildDataAccessException_The_record_was_not_found_;

                        break;
                    case DatabaseReturnCode.TimeStampMismatch:
                        message = Resources.DataAccess_BuildDataAccessException_The_record_was_updated_by_another_user_since_you_selected_it___Please_re_edit_this_record_and_attempt_to_save_it_again_;

                        break;
                    default:
                        message = String.Format(Resources.DataAccess_BuildDataAccessException_Value_not_programmed__please_see_administrator__Value____0_FormatString, returnCode);
                        break;
                }
            }

            return new DataAccessException(returnCode, source, message);
        }

        String GetIndexNameFromException(SqlException ex) {

            Int32 uniqueIndexPosition = ex.Message.ToLower().IndexOf("unique index '");

            if(uniqueIndexPosition > 0) {

                Int32 endOfUniqueIndexName = ex.Message.IndexOf("'", uniqueIndexPosition + 15);

                if(endOfUniqueIndexName > 0) {
                    return ex.Message.Substring(uniqueIndexPosition + 14, endOfUniqueIndexName - uniqueIndexPosition - 14);
                }

            } else {
                uniqueIndexPosition = ex.Message.ToLower().IndexOf("unique key constraint '");

                Int32 endOfUniqueIndexName = ex.Message.IndexOf("'", uniqueIndexPosition + 24);

                if(endOfUniqueIndexName > 0) {
                    return ex.Message.Substring(uniqueIndexPosition + 23, endOfUniqueIndexName - uniqueIndexPosition - 23);
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Local helper function to load command parameters into a command object.
        /// </summary>
        /// <param name="loadReturnValueAndReturnCodeParameter">if set to <c>true</c> [load return value and return code parameter].</param>
        /// <param name="cmd">Instantiated command object</param>
        /// <param name="parameters">Paramarry of sql client parameters</param>
        /// <remarks>This function was created to allow the loading of the parameters collection to be abstracted, making future modifications easier</remarks>
        void LoadCommandParameters(Boolean loadReturnValueAndReturnCodeParameter, SqlCommand cmd, params SqlParameter[] parameters) {

            if(loadReturnValueAndReturnCodeParameter) {
                cmd.Parameters.Add(SqlParameterAssistant.BuildSqlParameterReturnValue());
            }

            if(parameters != null) {

                foreach(SqlParameter p in parameters) {
                    cmd.Parameters.Add(p);
                }

            }

            if(loadReturnValueAndReturnCodeParameter) {
                cmd.Parameters.Add(SqlParameterAssistant.BuildSqlParameterReturnCode());
            }

        }

        Boolean ParseSqlClientExceptions(Exception ex, ref String dataBaseErrorMessage, ref DatabaseReturnCode databaseReturnCode) {
            dataBaseErrorMessage = String.Empty;

            if(ex is SqlException || ex.InnerException is SqlException) {

                SqlException sqlException;

                if(ex is SqlException) {
                    sqlException = (SqlException)ex;

                } else {
                    sqlException = (SqlException)ex.InnerException;
                }

                if(sqlException.Number == 547) {

                    databaseReturnCode = DatabaseReturnCode.ReferentialIntegrityViolated;

                    for(Int32 error = 0; error < sqlException.Errors.Count; error++) {
                        dataBaseErrorMessage += sqlException.Errors[error].Message + Environment.NewLine;
                    }

                    return true;

                }

                if(sqlException.Number == 2601 || sqlException.Number == 2627) {

                    databaseReturnCode = DatabaseReturnCode.DuplicateKeyViolation;

                    String indexName = GetIndexNameFromException(sqlException);

                    if(indexName.Length > 0) {

                        var sqlParam = new SqlParameter { SqlDbType = SqlDbType.VarChar, Direction = ParameterDirection.Input, ParameterName = "@INDEXNAME", Size = 250, Value = indexName };

                        //save our class instance's current state
                        Int32 saveAffectedRows = this.AffectedRows;

                        //===========================================================================================
                        //See the SystemGetIndexColumnNames Source.txt file in this project for the 
                        //  SystemGetIndexColumnNames stored procedure source code
                        //
                        //This stored procedure uses the GetReturnValue SQL Server function.  The source is in the
                        //  GetReturnValue SQL Server Function.txt file.
                        //===========================================================================================
                        //Good reuse of an open sql connection object
                        Object indexFields = this.ExecuteScaler(CommandType.StoredProcedure, "SystemGetIndexColumnNames", sqlParam);
                        //restore our class instance's current state
                        this.AffectedRows = saveAffectedRows;
                        _returnCode = DatabaseReturnCode.DuplicateKeyViolation;

                        if(indexFields != null) {
                            dataBaseErrorMessage = String.Format(Resources.DataAccess_ParseSqlClientExceptions_While_saving_this_record_there_was_a_violation_of_a_database_unique_index__1__1_The_following_field_s__make_up_the_unique_index__0___1__1_Please_see_your_administrator_for_data_entry_help_FormatString, Convert.ToString(indexFields), Environment.NewLine);
                            return true;

                        }

                        dataBaseErrorMessage = String.Format(Resources.DataAccess_ParseSqlClientExceptions_While_saving_this_record_there_was_a_violation_of_a_database_unique_index__0__0_The_system_was_unable_to_retrieve_the_column_names__0__0_Please_see_your_administrator_for_data_entry_help__0__0_FormatString, Environment.NewLine);

                        for(Int32 error = 0; error < sqlException.Errors.Count; error++) {
                            dataBaseErrorMessage += sqlException.Errors[error].Message + Environment.NewLine;
                        }

                        return true;
                    }

                    dataBaseErrorMessage = String.Format(Resources.DataAccess_ParseSqlClientExceptions_While_saving_this_record_there_was_a_violation_of_a_database_unique_index__0__0_The_system_was_unable_to_retrieve_the_column_names__0__0_Please_see_your_administrator_for_data_entry_help__0__0_FormatString, Environment.NewLine);

                    for(Int32 error = 0; error < sqlException.Errors.Count; error++) {
                        dataBaseErrorMessage += sqlException.Errors[error].Message + Environment.NewLine;
                    }
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Local helper function to load the class properties that are loaded after every data operation
        /// </summary>
        /// <param name="cmdParameters">Command paramertes collection</param>
        void SetClassPropertiesFromOutboundParameters(SqlParameterCollection cmdParameters) {
            this.AffectedRows = Convert.ToInt32(cmdParameters[SqlConstants.STRING_RETURN_VALUE].Value);
            _returnCode = (DatabaseReturnCode)(cmdParameters[SqlConstants.STRING_RETURN_CODE].Value);
        }

        #endregion

        #region  Execute Scalar

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
        public Object ExecuteScaler(CommandType commandType, String commandText) {
            return ExecuteScaler(commandType, commandText, 0, null);
        }

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
        public Object ExecuteScaler(CommandType commandType, String commandText, Int32 commandTimeOut, params SqlParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "ExecuteScaler", String.Format(Resources.DataAccess_ExecuteScaler_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "ExecuteScaler", Resources.DataAccess_ExecuteScaler_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(this.SqlConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    Object returnObject = cmd.ExecuteScalar();

                    if(cmd.CommandType == CommandType.StoredProcedure) {
                        SetClassPropertiesFromOutboundParameters(cmd.Parameters);

                        if(this.ReturnCode != DatabaseReturnCode.Successful) {
                            throw BuildDataAccessException(this.ReturnCode, "ExecuteScaler", "");
                        }
                    }
                    return returnObject;
                }
            }

            //connection will be controlled by this procedure, ie. connection state is closed
            using(this.SqlConnection) {
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    this.SqlConnection.Open();

                    Object returnObject = cmd.ExecuteScalar();

                    if(cmd.CommandType == CommandType.StoredProcedure) {
                        SetClassPropertiesFromOutboundParameters(cmd.Parameters);

                        if(this.ReturnCode != DatabaseReturnCode.Successful) {
                            throw BuildDataAccessException(this.ReturnCode, "ExecuteScaler", "");
                        }
                    }
                    return returnObject;
                }
            }
        }

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
        public Object ExecuteScaler(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return ExecuteScaler(commandType, commandText, 0, parameters);
        }

        #endregion

        #region  Execute Non Query

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
        public Int32 ExecuteNonQuery(CommandType commandType, String commandText) {
            return ExecuteNonQuery(commandType, commandText, 0, null);
        }

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
        public Int32 ExecuteNonQuery(CommandType commandType, String commandText, Int32 commandTimeOut, params SqlParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "ExecuteNonQuery", String.Format(Resources.DataAccess_ExecuteScaler_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "ExecuteNonQuery", Resources.DataAccess_ExecuteScaler_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(this.SqlConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    try {
                        this.AffectedRows = cmd.ExecuteNonQuery();

                        if(cmd.CommandType == CommandType.StoredProcedure) {
                            SetClassPropertiesFromOutboundParameters(cmd.Parameters);

                            if(this.ReturnCode != DatabaseReturnCode.Successful) {
                                throw BuildDataAccessException(this.ReturnCode, "ExecuteNonQuery", "");
                            }
                        }

                    } catch(Exception ex) {

                        //must catch all exceptions, some we'll just let go, others need to be processed
                        //but because exceptions now have wrappers we have catch and parse.  Thank you Microsoft :-(((
                        //in other words we can't catch the SQL Exception directly Thank you Microsoft :-(((
                        String strDataBaseErrorMessage = String.Empty;

                        if(ParseSqlClientExceptions(ex, ref strDataBaseErrorMessage, ref _returnCode)) {
                            throw BuildDataAccessException(_returnCode, "ExecuteNonQuery", strDataBaseErrorMessage);
                        }
                        throw;
                    }
                    return this.AffectedRows;
                }
            }

            //connection will be controlled by this procedure, ie. connection state is closed
            using(this.SqlConnection) {
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    this.SqlConnection.Open();

                    try {
                        this.AffectedRows = cmd.ExecuteNonQuery();

                        if(cmd.CommandType == CommandType.StoredProcedure) {
                            SetClassPropertiesFromOutboundParameters(cmd.Parameters);

                            if(this.ReturnCode != DatabaseReturnCode.Successful) {
                                throw BuildDataAccessException(this.ReturnCode, "ExecuteNonQuery", "");
                            }
                        }

                    } catch(Exception ex) {

                        //must catch all exceptions, some we'll just let go, others need to be processed
                        //but because exceptions now have wrappers we have catch and parse.  
                        //in other words we can't catch the SQL Exception directly Thank you Microsoft :-(((
                        String strDataBaseErrorMessage = String.Empty;

                        if(ParseSqlClientExceptions(ex, ref strDataBaseErrorMessage, ref _returnCode)) {
                            throw BuildDataAccessException(_returnCode, "ExecuteNonQuery", strDataBaseErrorMessage);
                        }
                        throw;
                    }
                    return this.AffectedRows;
                }
            }
        }

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
        public Int32 ExecuteNonQuery(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return ExecuteNonQuery(commandType, commandText, 0, parameters);
        }

        #endregion

        #region  Dataset

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
        public DataSet GetDataSet(CommandType commandType, String commandText, Int32 commandTimeOut, String[] tableNames, params SqlParameter[] parameters) {
            return GetDataSet(commandType, commandText, commandTimeOut, new DataSet(), tableNames, parameters);
        }

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
        public DataSet GetDataSet(CommandType commandType, String commandText, Int32 commandTimeOut, DataSet ds, String[] tableNames, params SqlParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "GetDataSet", String.Format(Resources.DataAccess_ExecuteScaler_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlConnection.State));
            }

            if(tableNames == null || tableNames.Length == 0) {
                throw new DataAccessException(DatabaseReturnCode.MissingTableName, "GetDataSet", Resources.DataAccess_GetDataSet_The_method_did_not_receive_any_TableNames___At_least_one_table_name_must_be_supplied_);
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "GetDataSet", Resources.DataAccess_ExecuteScaler_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(ds == null) {
                throw new DataAccessException(DatabaseReturnCode.MissingDataSetInstance, "GetDataSet", Resources.DataAccess_GetDataSet_The_method_did_not_receive_an_instianted_dataset__dataset_was_nothing___The_dataset_must_be_instianted_before_calling_this_procedure_or_else_use_one_of_the_other_overloads_for_returning_a_dataset_that_does_instiante_the_dataset_for_the_calling_code_);
            }

            if(this.SqlConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(false, cmd, parameters);

                    //Return value parameters not used
                    //LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    var da = new SqlDataAdapter(cmd);

                    if(cmd.CommandType == CommandType.StoredProcedure) {

                        Int32 i = 0;

                        foreach(String s in tableNames) {

                            if(i == 0) {
                                da.TableMappings.Add("Table", s);

                            } else {
                                da.TableMappings.Add("Table" + i, s);
                            }
                            i += 1;
                        }
                    }
                    da.Fill(ds);
                }

            } else {
                //connection will be controlled by this procedure, ie. connection state is closed
                using(this.SqlConnection) {
                    var cmd = new SqlCommand();
                    using(cmd) {
                        cmd.CommandText = commandText;
                        cmd.CommandType = commandType;
                        cmd.CommandTimeout = commandTimeOut;
                        cmd.Connection = this.SqlConnection;

                        LoadCommandParameters(false, cmd, parameters);

                        //Return value parameters not used
                        //LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                        var da = new SqlDataAdapter(cmd);

                        if(cmd.CommandType == CommandType.StoredProcedure) {

                            Int32 i = 0;

                            foreach(String s in tableNames) {

                                if(i == 0) {
                                    da.TableMappings.Add("Table", s);

                                } else {
                                    da.TableMappings.Add("Table" + i, s);
                                }
                                i += 1;
                            }
                        }
                        this.SqlConnection.Open();
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

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
        public DataSet GetDataSet(CommandType commandType, String commandText, String[] tableNames) {
            return GetDataSet(commandType, commandText, 0, new DataSet(), tableNames, null);
        }

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
        public DataSet GetDataSet(CommandType commandType, String commandText, String[] tableNames, params SqlParameter[] parameters) {
            return GetDataSet(commandType, commandText, 0, new DataSet(), tableNames, parameters);
        }

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
        public DataSet GetDataSet(CommandType commandType, String commandText, DataSet ds, String[] tableNames, params SqlParameter[] parameters) {
            return GetDataSet(commandType, commandText, 0, ds, tableNames, parameters);
        }

        #endregion

        #region  DataReader

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
        public SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior) {
            return GetDataReader(commandType, commandText, commandBehavior, 0, null);
        }

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
        public SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "GetDataReader", String.Format(Resources.DataAccess_ExecuteScaler_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlConnection.State));
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "GetDataReader", Resources.DataAccess_ExecuteScaler_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            var cmd = new SqlCommand { CommandText = commandText, CommandType = commandType, CommandTimeout = commandTimeOut, Connection = this.SqlConnection };

            LoadCommandParameters(false, cmd, parameters);

            //parameters not used
            //LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

            if(this.SqlConnection.State == ConnectionState.Closed) {
                this.SqlConnection.Open();
            }

            return cmd.ExecuteReader(commandBehavior);
        }

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
        public SqlDataReader GetDataReader(CommandType commandType, String commandText, CommandBehavior commandBehavior, params SqlParameter[] parameters) {
            return GetDataReader(commandType, commandText, commandBehavior, 0, parameters);
        }

        #endregion

        #region  DataTable

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
        public DataTable GetDataTable(CommandType commandType, String commandText, Int32 commandTimeOut, String tableName, params SqlParameter[] parameters) {
            return GetDataTable(commandType, commandText, commandTimeOut, new DataTable(), tableName, parameters);
        }

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
        public DataTable GetDataTable(CommandType commandType, String commandText, Int32 commandTimeOut, DataTable dt, String tableName, params SqlParameter[] parameters) {

            if(!IsConnectionStateValid(this.SqlConnection)) {
                throw new DataAccessException(DatabaseReturnCode.ConnectionStateError, "GetDataTable", String.Format(Resources.DataAccess_ExecuteScaler_The_sqlconnection_state_was__0____The_method_only_permits_the_sqlconnection_state_to_be_either_open_or_closed_only_FormatString, this.SqlConnection.State));
            }

            if(String.IsNullOrEmpty(tableName)) {
                throw new DataAccessException(DatabaseReturnCode.MissingTableName, "GetDataTable", Resources.DataAccess_GetDataTable_The_method_did_not_receive_the_TableName_);
            }

            if(String.IsNullOrEmpty(commandText)) {
                throw new DataAccessException(DatabaseReturnCode.MissingCommandText, "GetDataTable", Resources.DataAccess_ExecuteScaler_The_method_did_not_receive_any_command_text___Calling_code_must_supply_the_command_text_);
            }

            if(dt == null) {
                throw new DataAccessException(DatabaseReturnCode.MissingDataSetInstance, "GetDataTable", Resources.DataAccess_GetDataTable_The_method_did_not_receive_an_instianted_dataset__datatable_was_nothing___The_datatable_must_be_instianted_before_calling_this_procedure_or_else_use_one_of_the_other_overloads_for_returning_a_datatable_that_does_instiante_the_datatable_for_the_calling_code_);
            }

            dt.TableName = tableName;

            if(this.SqlConnection.State == ConnectionState.Open) {
                //connection is being controlled by the calling procedure
                var cmd = new SqlCommand();
                using(cmd) {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = commandTimeOut;
                    cmd.Connection = this.SqlConnection;

                    LoadCommandParameters(false, cmd, parameters);

                    //parameters not used for data tables
                    //LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                    var da = new SqlDataAdapter(cmd);

                    if(cmd.CommandType == CommandType.StoredProcedure) {
                        da.TableMappings.Add("Table", tableName);
                    }
                    da.Fill(dt);
                }

            } else {
                //connection will be controlled by this procedure, ie. connection state is closed
                using(this.SqlConnection) {
                    var cmd = new SqlCommand();
                    using(cmd) {
                        cmd.CommandText = commandText;
                        cmd.CommandType = commandType;
                        cmd.CommandTimeout = commandTimeOut;
                        cmd.Connection = this.SqlConnection;

                        LoadCommandParameters(false, cmd, parameters);

                        //parameters not used for data table
                        //LoadCommandParameters(cmd.CommandType == CommandType.StoredProcedure, cmd, parameters);

                        var da = new SqlDataAdapter(cmd);

                        if(cmd.CommandType == CommandType.StoredProcedure) {
                            da.TableMappings.Add("Table", tableName);
                        }

                        this.SqlConnection.Open();
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

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
        public DataTable GetDataTable(CommandType commandType, String commandText, String tableName) {
            return GetDataTable(commandType, commandText, 0, new DataTable(), tableName, null);
        }

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
        public DataTable GetDataTable(CommandType commandType, String commandText, String tableName, params SqlParameter[] parameters) {
            return GetDataTable(commandType, commandText, 0, new DataTable(), tableName, parameters);
        }

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
        public DataTable GetDataTable(CommandType commandType, String commandText, DataTable dt, String tableName, params SqlParameter[] parameters) {
            return GetDataTable(commandType, commandText, 0, dt, tableName, parameters);
        }

        #endregion
    }
}