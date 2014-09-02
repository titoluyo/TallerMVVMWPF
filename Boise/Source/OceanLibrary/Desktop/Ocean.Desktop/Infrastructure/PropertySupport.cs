using System;
using System.Linq.Expressions;
using System.Reflection;
using Ocean.Properties;


namespace Ocean.Infrastructure {

    /// <summary>
    /// Represents PropertySupport which provides LINQ expression parsing for INPC
    /// </summary>
    public class PropertySupport {

        /// <summary>
        /// Extracts the name of the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpresssion">The property expresssion.</param>
        /// <returns></returns>
        public static String ExtractPropertyName<T>(Expression<Func<T>> propertyExpresssion) {
            if (propertyExpresssion == null) {
                throw new ArgumentNullException("propertyExpresssion");
            }

            var memberExpression = propertyExpresssion.Body as MemberExpression;
            if (memberExpression == null) {
                throw new ArgumentException(Resources.PropertySupport_ExtractPropertyName_The_expression_is_not_a_member_access_expression_, "propertyExpresssion");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null) {
                throw new ArgumentException(Resources.PropertySupport_ExtractPropertyName_The_member_access_expression_does_not_access_a_property_, "propertyExpresssion");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic) {
                throw new ArgumentException(Resources.PropertySupport_ExtractPropertyName_The_referenced_property_is_a_static_property_, "propertyExpresssion");
            }

            return memberExpression.Member.Name;
        }
    }
}
