using System;
using System.ComponentModel;
using System.Windows.Data;

namespace Ocean.Wpf.Converters {

    /// <summary>
    /// Represents the FormattingConverter
    /// </summary>
    [ValueConversion(typeof(object), typeof(string))]
    public class FormattingConverter : IValueConverter {

        #region " Methods "

        /// <summary>
        ///     Takes a number, date or string and formats the value according to the paramenter and culture
        /// </summary>
        /// <param name="value" type="Object">
        ///     <para>
        ///         Number, date or string
        ///     </para>
        /// </param>
        /// <param name="targetType" type="System.Type">
        ///     <para>
        ///         This is automatically passed in and is the type of the target (calling) UI Element.
        ///     </para>
        /// </param>
        /// <param name="parameter" type="Object">
        ///     <para>
        ///        A .Net formatting code.  
        ///        Example, for currency pass the familar "{0:c}" like this : \{0:c\} or like this {}{0:c}
        ///        Both ways work.
        ///        You can also include a string like this, ' My Money \{0:c\}'
        ///        If you do not use the {}{0:c} syntax, then it is necessary to escape the "{" and "}" otherwise parser will get confused because "{" and "}" is used as delimiters in the Binding statement.
        ///        See the remarks below for an example of the Binding statement.
        ///     </para>
        /// </param>
        /// <param name="culture" type="System.Globalization.CultureInfo">
        ///     <para>
        ///         Not required to be passed in xaml code. WPF automatically passes the current culture info for you.
        ///     </para>
        /// </param>
        /// <returns>
        ///     The value, formatted according to the parameter.
        /// </returns>
        /// <remarks>
        /// Usage Examples:
        /// Text="{Binding Path=BirthDay, Converter={StaticResource FormattingConverter}, ConverterParameter=\{0:M\}, Mode=Default}"
        /// Text="{Binding Path=BirthDay, Converter={StaticResource FormattingConverter}, ConverterParameter=\{0:R\}, Mode=Default}"
        /// Text="{Binding Path=BirthDay, Converter={StaticResource FormattingConverter}, ConverterParameter='{}{0:ddd, d MMM yyyy} is my day!', Mode=Default}"
        /// </remarks>
        public object Convert(Object value, System.Type targetType, Object parameter, System.Globalization.CultureInfo culture) {

            if (value == null) {
                return null;
            }


            if (parameter != null) {
                var formatString = parameter.ToString();

                if (!String.IsNullOrEmpty(formatString)) {
                    return string.Format(culture, formatString, value);
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Attempts to convert the value back using a type specific TypeConverter
        /// </summary>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            var typeConverter = TypeDescriptor.GetConverter(targetType);
            object returnValue = null;

            if (typeConverter != null && typeConverter.CanConvertFrom(value.GetType())) {
                try {
                    returnValue = typeConverter.ConvertFrom(value);
                } catch (Exception) {
                    //HACK - FormattingConverter exception handler.  Developers you have two options here.
                    //1.  Return nothing which in effect wipes out the offending text in the binding control
                    //
                    //objReturnValue = Nothing
                    //
                    //
                    //2.  Return the value.  Then allow the data binding to fail further down the chain, ie. when it attempts to bind to the entity object.  This failure will be handled by the data binding pipeline.
                    //
                    returnValue = value;
                }
            }
            return returnValue;
        }

        #endregion
    }
}