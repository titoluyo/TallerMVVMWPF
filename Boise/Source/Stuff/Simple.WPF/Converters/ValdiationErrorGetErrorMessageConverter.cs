
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Simple.WPF.Converters {

    /// <summary>
    /// Converter that returns the ValidationError.ErrorContent or the first inner exception message.
    /// This converter is handly because it will return the message from an InnerException or the ErrorContent. 
    /// Some data binding exceptions are package in a System.Reflection.TargetInvocationException and this converter will return the message from the actual exception rather than the useless TargetInvocationException message.
    /// This converter usefullness is demonstrated when IDataErrorInfo property validation routines throw exceptions. Those exceptions will bubble throught the WPF data binding pipeline and the InnerException will be displayed using this converter.
    /// </summary>
    [ValueConversion(typeof(ValidationError), typeof(String))]
    public class ValdiationErrorGetErrorMessageConverter : IValueConverter {

        #region Methods

        public object Convert(Object value, System.Type targetType, Object parameter, System.Globalization.CultureInfo culture) {

            if (value == null || !(value is ReadOnlyObservableCollection<ValidationError>)) {
                return null;
            }
            StringBuilder sb = new StringBuilder(1024);

            foreach (ValidationError ve in (ReadOnlyObservableCollection<ValidationError>)value) {

                if (ve.Exception == null || ve.Exception.InnerException == null) {
                    sb.AppendLine(ve.ErrorContent.ToString());
                } else {
                    sb.AppendLine(ve.Exception.InnerException.Message);
                }
            }

            //remove the last line feed
            if (sb.Length > 2) {
                sb.Length -= 2;
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }

        #endregion
    }
}
