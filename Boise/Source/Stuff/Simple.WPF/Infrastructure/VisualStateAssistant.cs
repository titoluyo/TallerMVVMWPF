
using System;
using System.Windows;

namespace Simple.WPF.Infrastructure {

    /// <summary>
    /// Represents a Visual State Assistant.  
    /// This is an attached property allowing the ViewModel to data bind a property to change Visual States
    /// </summary>
    public class VisualStateAssistant : DependencyObject {
        public static readonly DependencyProperty CurrentVisualStateProperty =
            DependencyProperty.RegisterAttached("CurrentVisualState", typeof(String), typeof(VisualStateAssistant), new PropertyMetadata(TransitionToState));

        public static String GetCurrentVisualState(DependencyObject obj) {
            return (String)obj.GetValue(CurrentVisualStateProperty);
        }

        public static void SetCurrentVisualState(DependencyObject obj, String value) {
            obj.SetValue(CurrentVisualStateProperty, value);
        }
        /// <summary>
        /// This private method does the work to change the Visual State on the UI
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        static void TransitionToState(object sender, DependencyPropertyChangedEventArgs args) {
            var fe = sender as FrameworkElement;

            if(fe != null) {
                VisualStateManager.GoToElementState(fe, (String)args.NewValue, true);
            } else {
                throw new ArgumentException("CurrentVisualState is only supported on the FrameworkElement type");
            }
        }
    }
}
