using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Ocean.Sql {

    /// <summary>
    /// Represents DataRowReflectionBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DataRowReflectionBuilder<T> where T : class, new() {

        static readonly Dictionary<Type, DataRowReflectionBuilder<T>> _DataRowReflectionBuilders = new Dictionary<Type, DataRowReflectionBuilder<T>>();
        PropertyInfo[] _objProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRowReflectionBuilder&lt;T&gt;"/> class.
        /// </summary>
        DataRowReflectionBuilder() { }

        /// <summary>
        /// Builds the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public T Build(DataRow row) {

            var returnValue = new T();

            for(Int32 i = 0; i < row.Table.Columns.Count; i++) {

                if(_objProperties[i] != null && !(Convert.IsDBNull(row[i]))) {
                    _objProperties[i].SetValue(returnValue, row[i], null);
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public static DataRowReflectionBuilder<T> CreateBuilder(DataRow row) {

            DataRowReflectionBuilder<T> builder;

            if(!(_DataRowReflectionBuilders.TryGetValue(typeof(T), out builder))) {
                builder = new DataRowReflectionBuilder<T>();
            }

            builder._objProperties = new PropertyInfo[row.Table.Columns.Count];

            for(Int32 i = 0; i < row.Table.Columns.Count; i++) {
                builder._objProperties[i] = typeof(T).GetProperty(row.Table.Columns[i].ColumnName);
            }

            return builder;
        }
    }
}