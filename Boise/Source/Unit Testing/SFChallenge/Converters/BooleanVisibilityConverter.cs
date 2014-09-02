using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace SFChallenge.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanVisibilityConverter : IValueConverter
    {
        public BooleanVisibilityConverter()
        {
            this.WhenTrue = Visibility.Visible;
            this.WhenFalse = Visibility.Hidden;
            this.WhenNull = Visibility.Hidden;
        }

        public Visibility WhenTrue { get; set; }

        public Visibility WhenFalse { get; set; }

        public Visibility WhenNull { get; set; }


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? source = (bool?)value;

            if (source.HasValue)
            {
                return (source.Value == true) ? this.WhenTrue : this.WhenFalse;
            }
            else
            {
                return this.WhenNull;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility source = (Visibility)value;

            if (source == this.WhenTrue)
            {
                return true;
            }

            if (source == this.WhenFalse)
            {
                return false;
            }

            if (source == this.WhenNull)
            {
                return null;
            }

            return value;
        }
    }
}
