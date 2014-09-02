using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Wpf.Common.Infrastructure {
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
                throw new InvalidCastException("propertyExpresssion.Body is not a MemberExpression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null) {
                throw new InvalidCastException("propertyExpresssion.Member is not a PropertyInfo");
            }

            var getMethod = property.GetGetMethod(true);
            if (getMethod.IsStatic) {
                throw new InvalidOperationException("referenced property is static");
            }

            return memberExpression.Member.Name;
        }
    }
}
