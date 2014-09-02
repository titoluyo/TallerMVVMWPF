
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Reflection;

namespace Ocean.SqlCe {

    /// <summary>
    /// Represents DataReaderReflectionBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataReaderReflectionBuilder<T> where T: class, new() {

        static readonly Dictionary<Type, DataReaderReflectionBuilder<T>> _dataReaderReflectionBuilders = new Dictionary<Type, DataReaderReflectionBuilder<T>>();
        PropertyInfo[] _objProperties;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReaderReflectionBuilder&lt;T&gt;"/> class.
        /// </summary>
        DataReaderReflectionBuilder() { }

        /// <summary>
        /// Builds the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public T Build(SqlCeDataReader reader) {

            var returnValue = (T)(Activator.CreateInstance(typeof(T)));

            for(Int32 i = 0; i < reader.FieldCount; i++) {
                if(_objProperties[i] != null && !(reader.IsDBNull(i))) {
                    _objProperties[i].SetValue(returnValue, reader[i], null);
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Creates the builder.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public static DataReaderReflectionBuilder<T> CreateBuilder(SqlCeDataReader reader) {

            DataReaderReflectionBuilder<T> builder;

            if(!(_dataReaderReflectionBuilders.TryGetValue(typeof(T), out builder))) {
                builder = new DataReaderReflectionBuilder<T>();
            }

            builder._objProperties = new PropertyInfo[reader.FieldCount];

            for(Int32 i = 0; i < reader.FieldCount; i++) {
                builder._objProperties[i] = typeof(T).GetProperty(reader.GetName(i));
            }

            return builder;
        }
    }
}