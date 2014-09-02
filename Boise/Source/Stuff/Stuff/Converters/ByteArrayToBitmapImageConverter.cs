
using System;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace Stuff.Converters {

    /// <summary>
    /// Represents a ByteArrayToBitmapImageConverter
    /// </summary>
    [ValueConversion(typeof(Byte[]), typeof(BitmapImage))]
    public class ByteArrayToBitmapImageConverter : IValueConverter {
        static BitmapImage _fillerImage = null;

        public object Convert(Object value, Type targetType, Object parameter, System.Globalization.CultureInfo culture) {

            if (value == null) {

                //HACK - this is how to have sample data image place holders when your runtime images are byte streams and not actual images.
                //       we will return an image from the application resources.
                if (_fillerImage == null)
                    _fillerImage = LoadImageFromResource("images/dummyfiller.png");
                return _fillerImage;
            } else if (value is Byte[]) {
                BitmapImage img = new BitmapImage();

                using (MemoryStream ms = new MemoryStream((Byte[])value)) {

                    ms.Seek(0, SeekOrigin.Begin);
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = ms;
                    img.EndInit();
                    img.Freeze();
                }
                return img;
            } else {
                return null;
            }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
        BitmapImage LoadImageFromResource(String path) {
            StreamResourceInfo sri = null;
            BitmapImage img = null;

            try {
                sri = Application.GetResourceStream(new Uri(path, UriKind.Relative));

                if (sri.ContentType.IndexOf("image") >= 0) {
                    img = new BitmapImage();
                    img.BeginInit();
                    img.CacheOption = BitmapCacheOption.OnLoad;
                    img.StreamSource = sri.Stream;
                    img.EndInit();
                    img.Freeze();
                }
            } catch (Exception) {
                //ignore
            } finally {

                if (sri != null) {
                    sri.Stream.Close();
                    sri.Stream.Dispose();
                }
            }
            return img;
        }
    }
}
