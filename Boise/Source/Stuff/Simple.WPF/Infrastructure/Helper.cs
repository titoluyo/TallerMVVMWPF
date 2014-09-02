
using System.Windows;
using System.Windows.Media;

namespace Simple.WPF.Infrastructure {

    /// <summary>
    /// Represents a Helper
    /// </summary>
    public static class Helper {

        /// <summary>
        /// Finds the first visual child of the <see cref="Type"/> T.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> to find.</typeparam>
        /// <param name="dpo"><see cref="DependencyObject"/> to start the searching down the tree of elements.</param>
        /// <returns>First <see cref="DependencyObject"/> of <see cref="Type"/> T.</returns>
        public static T FindVisualChild<T>(DependencyObject dpo) where T : DependencyObject {

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(dpo) - 1; i++) {
                DependencyObject child = VisualTreeHelper.GetChild(dpo, i);

                if (child != null && child is T) {
                    return (T)child;
                } else {
                    T childOfChild = FindVisualChild<T>(child);

                    if (childOfChild != null) {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
