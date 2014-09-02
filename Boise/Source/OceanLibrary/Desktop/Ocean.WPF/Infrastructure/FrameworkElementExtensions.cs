using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents FrameworkElementExtensions
    /// </summary>
    public static class FrameworkElementExtensions {

        /// <summary>
        /// Initializes the <see cref="FrameworkElementExtensions"/> class.
        /// </summary>
        static FrameworkElementExtensions() { }

        /// <summary>
        /// Returns a BitmapSource for the FrameworkElement
        /// </summary>
        /// <param name="element">The element.</param>
        public static BitmapSource GetBitmapSource(this FrameworkElement element) {
            var visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();
            var elementBrush = new VisualBrush(element);

            Int32 w = Convert.ToInt32(element.ActualWidth);
            Int32 h = Convert.ToInt32(element.ActualHeight);

            context.DrawRectangle(elementBrush, null, new Rect(0, 0, w, h));
            context.Close();

            var bitmap = new RenderTargetBitmap(w, h, 96, 96, PixelFormats.Default);
            bitmap.Render(visual);
            return bitmap;
        }
    }
}
