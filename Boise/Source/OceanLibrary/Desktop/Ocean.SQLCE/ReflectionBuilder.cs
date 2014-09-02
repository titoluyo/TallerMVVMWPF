
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Reflection;
using Ocean.OceanValidation;

namespace Ocean.SqlCe {

    //If you cringe at the thought of using Reflection at runtime in a loop,
    //  please check out the performance numbers of this code, then start using it.
    //This code caches the reflected type and resuses it, so you only pay the cost once.
    //
    //http://karlshifflett.wordpress.com/2008/04/28/sample-series-bench-marking-Object-loading-application-ii/


    /// <summary>
    /// Represents ReflectionBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReflectionBuilder<T> where T : class, new() {

        #region  Declarations

        PropertyInfo[] _builderProperties;
        static readonly Dictionary<Type, PropertyInfo[]> _reflectionBuilderProperties = new Dictionary<Type, PropertyInfo[]>();

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReflectionBuilder&lt;T&gt;"/> class.
        /// </summary>
        public ReflectionBuilder() { }

        #endregion

        #region  DataRow

        /// <summary>
        /// Builds the specified row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public T Build(DataRow row) {

            if(_builderProperties == null) {
                CreateBuilder(row);
            }

            var returnValue = (T)(Activator.CreateInstance(typeof(T)));

            //this method prevents any validation code from running during loading
            MethodInfo mi = typeof(T).GetMethod(SqlConstants.STRING_BEGINLOADING);

            if(mi != null) {
                mi.Invoke(returnValue, null);
            }

            for(Int32 i = 0; i < row.Table.Columns.Count; i++) {

// ReSharper disable PossibleNullReferenceException
                if(_builderProperties[i] != null && !(Convert.IsDBNull(row[i]))) {
// ReSharper restore PossibleNullReferenceException
                    _builderProperties[i].SetValue(returnValue, row[i], null);
                }
            }

            //this sets the ActiveRuleSet property on the Object to Update
            PropertyInfo prop = typeof(T).GetProperty(SqlConstants.STRING_ACTIVERULESET);

            if(prop != null) {
                prop.SetValue(returnValue, ValidationConstants.STR_UPDATE, null);
            }

            //this method marks the Object as not dirty and turns all the features back on
            mi = typeof(T).GetMethod(SqlConstants.STRING_ENDLOADING);

            if(mi != null) {
                mi.Invoke(returnValue, null);
            }

            return returnValue;
        }

        void CreateBuilder(DataRow row) {

            if(_reflectionBuilderProperties.TryGetValue(typeof(T), out _builderProperties)) {
                return;
            }

            _builderProperties = new PropertyInfo[row.Table.Columns.Count];

            for(Int32 i = 0; i < row.Table.Columns.Count; i++) {
                _builderProperties[i] = typeof(T).GetProperty(row.Table.Columns[i].ColumnName);
            }

            _reflectionBuilderProperties.Add(typeof(T), _builderProperties);
        }

        #endregion

        #region  SqlDataReader Builder

        /// <summary>
        /// Builds the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        public T Build(SqlCeDataReader reader) {

            if(_builderProperties == null) {
                CreateBuilder(reader);
            }

            var returnValue = (T)(Activator.CreateInstance(typeof(T)));

            //this method prevents any validation code from running during loading
            MethodInfo mi = typeof(T).GetMethod(SqlConstants.STRING_BEGINLOADING);

            if(mi != null) {
                mi.Invoke(returnValue, null);
            }

            for(Int32 intX = 0; intX < reader.FieldCount; intX++) {

// ReSharper disable PossibleNullReferenceException
                if(_builderProperties[intX] != null && !(reader.IsDBNull(intX))) {
// ReSharper restore PossibleNullReferenceException
                    _builderProperties[intX].SetValue(returnValue, reader[intX], null);
                }
            }

            //this sets the ActiveRuleSet property on the Object to Update
            PropertyInfo prop = typeof(T).GetProperty(SqlConstants.STRING_ACTIVERULESET);

            if(prop != null) {
                prop.SetValue(returnValue, ValidationConstants.STR_UPDATE, null);
            }

            //this method marks the Object as not dirty and turns all the features back on
            mi = typeof(T).GetMethod(SqlConstants.STRING_ENDLOADING);

            if(mi != null) {
                mi.Invoke(returnValue, null);
            }

            return returnValue;
        }

        void CreateBuilder(SqlCeDataReader reader) {

            if(_reflectionBuilderProperties.TryGetValue(typeof(T), out _builderProperties)) {
                return;
            }

            _builderProperties = new PropertyInfo[reader.FieldCount];

            for(Int32 i = 0; i < reader.FieldCount; i++) {
                _builderProperties[i] = typeof(T).GetProperty(reader.GetName(i));
            }

            _reflectionBuilderProperties.Add(typeof(T), _builderProperties);
        }

        #endregion
    }
}