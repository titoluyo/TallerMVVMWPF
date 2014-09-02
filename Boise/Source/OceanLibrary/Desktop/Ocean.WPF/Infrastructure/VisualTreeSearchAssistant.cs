using System.Windows;
using System.Windows.Media;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents VisualTreeSearchAssistant
    /// </summary>
    public static class VisualTreeSearchAssistant {

        /// <summary>
        /// Initializes the <see cref="VisualTreeSearchAssistant"/> class.
        /// </summary>
        static VisualTreeSearchAssistant() { }

        /// <summary>
        /// Finds the ancestor.
        /// </summary>
        /// <typeparam name="TParentItem">Type to match</typeparam>
        /// <param name="dependencyObject">The obj dependency object.</param>
        /// <returns></returns>
        public static TParentItem FindAncestor<TParentItem>(DependencyObject dependencyObject) where TParentItem : DependencyObject {

            while((dependencyObject) != null) {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

                if(dependencyObject is TParentItem) {
                    return dependencyObject as TParentItem;
                }
            }
            return default(TParentItem);
        }

        /// <summary>
        /// Finds the visual child.
        /// </summary>
        /// <typeparam name="TChildItem">The type of the child item.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static TChildItem FindVisualChild<TChildItem>(DependencyObject dependencyObject) where TChildItem : DependencyObject {

            for(int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++) {

                DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);

                if(child != null && child is TChildItem) {
                    return (TChildItem)child;

                }
                var childOfChild = FindVisualChild<TChildItem>(child);

                if(childOfChild != null) {
                    return childOfChild;
                }
            }

            return default(TChildItem);
        }
    }
}
