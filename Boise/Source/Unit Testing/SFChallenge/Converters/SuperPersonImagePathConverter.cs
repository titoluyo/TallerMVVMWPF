using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using SFChallenge.Model;
using System.Windows.Media;

namespace SFChallenge.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class SuperPersonImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SuperPerson superPerson = value as SuperPerson;

            if (superPerson == null)
            {
                return null;
            }

            return string.Format("Images/Avatars/{0}.png", superPerson.Name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
