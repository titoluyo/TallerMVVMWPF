using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ocean.Wpf.Converters {
    /// <summary>
    /// Represents a MinDateToVisibilityConverter
    /// </summary>
    [ValueConversion(typeof(DateTime), typeof(Visibility))]
    public class MinDateToVisibilityConverter : IValueConverter {

        /// <summary>
        /// Checks the value of Date.  If null or minimum date returns Visiblity.Collapsed otherwise returns Visibility.Visible
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) {
            if (value == null || !(value is DateTime)) {
                return Visibility.Collapsed;
            }
            return (DateTime)value == DateTime.MinValue ? Visibility.Collapsed : Visibility.Visible;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// throws NotImplementedException if called
        /// </returns>
        public object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
