
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlServerCe;

namespace Ocean.SqlCe {

    /// <summary>
    /// Represents EntityLoader, class provides simple methods to load business entity Object(s) and to run the ExecuteNonQuery method.
    /// </summary>
    /// <typeparam name="T">Type of entity Object to load.</typeparam>
    public class EntityLoader<T> where T : class, new() {
        
        #region Declarations

        Int32 _affectedRows = -1;
        readonly IDataAccess _dataAccess;

        #endregion //Declarations
        
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

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public EntityLoader(String connectionString) {
            _dataAccess = new DataAccess(connectionString);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityLoader&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="sqlCeConnection">The SQL ce connection.</param>
        public EntityLoader(SqlCeConnection sqlCeConnection) {
            _dataAccess = new DataAccess(sqlCeConnection);
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
        /// Loads the list.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<T> LoadList(String commandText, System.Data.CommandBehavior commandBehavior, params SqlCeParameter[] parameters) {

            var result = new List<T>();
            using(SqlCeDataReader reader = _dataAccess.GetDataReader(commandText, commandBehavior, parameters)) {

                var objBuilder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    result.Add(objBuilder.Build(reader));
                }
            }
            _affectedRows = result.Count;
            return result;
        }

        /// <summary>
        /// Loads the observable collection.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public ObservableCollection<T> LoadObservableCollection(String commandText, System.Data.CommandBehavior commandBehavior, params SqlCeParameter[] parameters) {

            var objResult = new ObservableCollection<T>();
            using(SqlCeDataReader reader = _dataAccess.GetDataReader(commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    objResult.Add(builder.Build(reader));
                }
            }
            _affectedRows = objResult.Count;
            return objResult;
        }

        /// <summary>
        /// Loads the one.
        /// </summary>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T LoadOne(String commandText, System.Data.CommandBehavior commandBehavior, params SqlCeParameter[] parameters) {
            _affectedRows = 0;
            using(SqlCeDataReader reader = _dataAccess.GetDataReader(commandText, commandBehavior, parameters)) {

                var builder = new ReflectionBuilder<T>();

                while(reader.Read()) {
                    _affectedRows += 1;
                    return builder.Build(reader);
                }
            }
            return default(T);
        }

        #endregion
    }
}