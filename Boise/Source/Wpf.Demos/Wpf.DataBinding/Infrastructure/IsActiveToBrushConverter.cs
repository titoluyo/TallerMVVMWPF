using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Wpf.DataBinding.Infrastructure {

    [ValueConversion(typeof(Boolean), typeof(Brush))]
    public class IsActiveToBrushConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null && value is Boolean && (Boolean)value) {
                return new SolidColorBrush(Colors.Black);
            }
            return new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
