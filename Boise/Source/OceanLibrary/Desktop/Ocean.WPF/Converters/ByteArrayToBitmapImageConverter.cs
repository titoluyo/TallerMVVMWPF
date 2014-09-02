using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Ocean.Wpf.Converters {

    /// <summary>
    /// Represents a ByteArrayToBitmapImageConverter
    /// </summary>
    [ValueConversion(typeof(Byte[]), typeof(Byte[]))]
    [ValueConversion(typeof(Byte[]), typeof(BitmapImage))]
    public class ByteArrayToBitmapImageConverter : IValueConverter {

        static BitmapImage _fillerImage;

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

                //HACK - this is how to have sample data image place holders when your runtime images are byte streams and not actual images.
                //       we will return an image from the application resources.
                return _fillerImage ?? (_fillerImage = LoadImageFromResource("resources/images/dummyfiller.png"));
            }

            if(value is Byte[]) {
                //
                //HACK - FYI:  the WPF image control is pretty smart.
                //       You can just return the Byte array and the Image control will consume it and display the image.
                //
                return value;

                //HACK - this is how to create a BitmapImage from a Byte array.  
                //However we don't need to use this code.
                //
                //BitmapImage img = new BitmapImage();

                //using (MemoryStream ms = new MemoryStream((Byte[])value)) {

                //    ms.Seek(0, SeekOrigin.Begin);
                //    img.BeginInit();
                //    img.CacheOption = BitmapCacheOption.OnLoad;
                //    img.StreamSource = ms;
                //    img.EndInit();
                //    img.Freeze();
                //}
                //return img;
            }
            return null;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {
            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Loads the image from resource.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        BitmapImage LoadImageFromResource(String path) {
            StreamResourceInfo sri = null;
            BitmapImage img = null;

            try {
                sri = Application.GetResourceStream(new Uri(path, UriKind.Relative));

                if(sri != null && sri.ContentType.IndexOf("image") >= 0) {
                    img = new BitmapImage();
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = sri.Stream;
                    img.EndInit();
                    img.Freeze();
                }
                // ReSharper disable EmptyGeneralCatchClause
            } catch(Exception) {
                // ReSharper restore EmptyGeneralCatchClause
                //ignore
            } finally {

                if(sri != null) {
                    sri.Stream.Close();
                    sri.Stream.Dispose();
                }
            }
            return img;
        }
    }
}
