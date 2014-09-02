
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Ocean.Infrastructure;

namespace Ocean.Sql {

    /// <summary>
    /// Represents EntityLoader, provides simple methods to load business entity object(s)
    /// </summary>
    /// <typeparam name="T">Type of entity object to load.</typeparam>
    public sealed class EntityLoader<T> : IEntityLoader<T> where T : class, new() {

        #region  Declarations

        DatabaseReturnCode _returnCode = DatabaseReturnCode.NotSet;
        Int32 _affectedRows = -1;
        readonly IDataAccess _dataAccess;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the number of rows affected by the data operation.
        /// </summary>
        /// <returns>Integer</returns>
        public Int32 AffectedRows {
            get {
                return _affectedRows;
            }
        }

        /// <summary>
        /// Gets the ReturnCode from the data operation
        /// </summary>
        /// <returns>DatabaseReturnCode</returns>
        public DatabaseReturnCode ReturnCode {
            get {
                return _returnCode;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader&lt;T&gt;"/> class.  Constructor used by other Data Layer classes when working with transactions or multiple operations using the same connection
        /// </summary>
        /// <param name="cn">SqlConnection</param>
        public EntityLoader(SqlConnection cn) {
            _dataAccess = new DataAccess(cn);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="connectionString"></param>
        public EntityLoader(String connectionString) {
            _dataAccess = new DataAccess(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public EntityLoader(IDataAccess dataAccess) {
            _dataAccess = dataAccess;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Loads an IList, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IList<T> LoadList(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return LoadList(commandType, commandText, CommandBehavior.CloseConnection | CommandBehavior.SingleResult, 0, parameters);

        }

        /// <summary>
        /// Loads an IList using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>IList</returns>
        public IList<T> LoadList(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters) {

            var result = new List<T>();
            using(SqlDataReader reader = _dataAccess.GetDataReader(commandType, commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    result.Add(builder.Build(reader));
                }
            }
            _affectedRows = result.Count;
            _returnCode = DatabaseReturnCode.Successful;
            return result;
        }

        /// <summary>
        /// Loads an IQueryable, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IQueryable<T> LoadQueryable(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return LoadQueryable(commandType, commandText, CommandBehavior.CloseConnection | CommandBehavior.SingleResult, 0, parameters);
        }

        /// <summary>
        /// Loads an IQueryable using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="commandTimeOut">The command time out.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IQueryable</returns>
        public IQueryable<T> LoadQueryable(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters) {

            var result = new List<T>();
            using(SqlDataReader reader = _dataAccess.GetDataReader(commandType, commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    result.Add(builder.Build(reader));
                }
            }
            _affectedRows = result.Count;
            _returnCode = DatabaseReturnCode.Successful;
            return result.AsQueryable();
        }

        /// <summary>
        /// Loads an ObservableCollection, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public ObservableCollection<T> LoadObservableCollection(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return LoadObservableCollection(commandType, commandText, CommandBehavior.CloseConnection | CommandBehavior.SingleResult, 0, parameters);
        }

        /// <summary>
        /// Loads an ObservableCollection using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>ObservableCollection</returns>
        public ObservableCollection<T> LoadObservableCollection(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters) {

            var result = new ObservableCollection<T>();
            using(SqlDataReader reader = _dataAccess.GetDataReader(commandType, commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    result.Add(builder.Build(reader));
                }
            }
            _affectedRows = result.Count;
            _returnCode = DatabaseReturnCode.Successful;
            return result;
        }

        /// <summary>
        /// Loads an instane of T, closes the database connection, assumes a single result set, assumes a single row, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T LoadOne(CommandType commandType, String commandText, params SqlParameter[] parameters) {
            return LoadOne(commandType, commandText, CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow, 0, parameters);
        }

        /// <summary>
        /// Loads an instane of T using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Instance of T</returns>
        public T LoadOne(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters) {
            _affectedRows = 0;
            using(SqlDataReader reader = _dataAccess.GetDataReader(commandType, commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    _affectedRows += 1;
                    _returnCode = DatabaseReturnCode.Successful;
                    return builder.Build(reader);
                }
            }
            _returnCode = DatabaseReturnCode.RecordNotFound;
            return default(T);
        }

        #endregion
    }
}