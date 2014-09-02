using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Ocean.Wpf.Converters {
    /// <summary>
    /// Converter that returns the ValidationError.ErrorContent or the first inner exception message.
    /// This converter is handly because it will return the message from an InnerException or the ErrorContent.  
    /// Some data binding exceptions are package in a System.Reflection.TargetInvocationException and this converter will return the message from the actual exception rather than the useless TargetInvocationException message.
    /// This converter usefullness is demonstrated when IDataErrorInfo property validation routines throw exceptions.  Those exceptions will bubble throught the WPF data binding pipeline and the InnerException will be displayed using this converter.
    /// </summary>
    [ValueConversion(typeof(ValidationError), typeof(string))]
    public class ValdiationErrorGetErrorMessageConverter : IValueConverter {

        #region  Methods

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {

            if(value == null) {
                return null;
            }

            var sb = new System.Text.StringBuilder(1024);

            foreach(ValidationError objVb in (ReadOnlyObservableCollection<ValidationError>)value) {

                if(objVb.Exception == null || objVb.Exception.InnerException == null) {
                    sb.AppendLine(objVb.ErrorContent.ToString());

                } else {
                    sb.AppendLine(objVb.Exception.InnerException.Message);
                }

            }

            //remove the last line feed
            if(sb.Length > 2) {
                sb.Length -= 2;
            }

            return sb.ToString();
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