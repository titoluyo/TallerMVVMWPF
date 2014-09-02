using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Ocean.Infrastructure;

namespace Ocean.Sql {

    /// <summary>
    /// Represents IEntityLoader contact, provides simple methods to load business entity object(s)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntityLoader<T> where T : class, new() {
        /// <summary>
        /// Gets the number of rows affected by the data operation.
        /// </summary>
        /// <returns>Integer</returns>
        Int32 AffectedRows { get; }

        /// <summary>
        /// Gets the ReturnCode from the data operation
        /// </summary>
        /// <returns>DatabaseReturnCode</returns>
        DatabaseReturnCode ReturnCode { get; }

        /// <summary>
        /// Loads an IList, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IList<T> LoadList(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an IList using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>IList</returns>
        IList<T> LoadList(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an IQueryable, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        IQueryable<T> LoadQueryable(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an IQueryable using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="commandBehavior">The command behavior.</param>
        /// <param name="commandTimeOut">The command time out.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>IQueryable</returns>
        IQueryable<T> LoadQueryable(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an ObservableCollection, closes the database connection, assumes a single result set, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        ObservableCollection<T> LoadObservableCollection(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an ObservableCollection using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>ObservableCollection</returns>
        ObservableCollection<T> LoadObservableCollection(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an instane of T, closes the database connection, assumes a single result set, assumes a single row, uses the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        T LoadOne(CommandType commandType, String commandText, params SqlParameter[] parameters);

        /// <summary>
        /// Loads an instane of T using the connection passed in the constructor
        /// </summary>
        /// <param name="commandType">SQL command type, normally this is stored procedure</param>
        /// <param name="commandText">Command text, normally this is the stored procedure name</param>
        /// <param name="commandBehavior">SQLDataReader CommandBeharior</param>
        /// <param name="commandTimeOut">Time out value for this request</param>
        /// <param name="parameters">Param array of SQL Parameter objects.  For inbound parameters you must set the value of the parameter</param>
        /// <returns>Instance of T</returns>
        T LoadOne(CommandType commandType, String commandText, CommandBehavior commandBehavior, Int32 commandTimeOut, params SqlParameter[] parameters);
    }
}