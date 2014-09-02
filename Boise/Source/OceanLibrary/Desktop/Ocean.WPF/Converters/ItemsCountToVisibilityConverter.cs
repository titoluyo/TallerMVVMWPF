using System;
using System.Windows;
using System.Windows.Data;

namespace Ocean.Wpf.Converters {
    /// <summary>
    /// Represents ItemsCountToVisibilityConverter
    /// </summary>
    [ValueConversion(typeof(Int32), typeof(Visibility))]
    public class ItemsCountToVisibilityConverter : IValueConverter {

        #region  Methods

        /// <summary>
        /// Converter takes any integer as the value, if that integer is equal to or less than zero then converter returns either Collapsed or the value passed as the parameter, otherwise returns Visible.
        /// </summary>
        /// <param name="value">Integer</param>
        /// <param name="targetType">UI Element</param>
        /// <param name="parameter">Collapsed, Hidden or else nothing</param>
        /// <param name="culture"></param>
        /// <returns>Visible, Hidden or Collapsed</returns>
        public object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {

            Int32 testValue;
            if(value == null || Int32.TryParse(value.ToString(), out testValue) && testValue <= 0) {

                if(parameter == null) {
                    return "Collapsed";
                }

                if(parameter is String) {

                    var s = parameter as String;

                    if(s == "Collapsed" || s == "Hidden" || s == "Visible") {
                        return parameter;
                    }

                }
                return "Collapsed";
            }
            return "Visible";
        }

        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public Object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}

