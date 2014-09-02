using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.Behaviors {

    /// <summary>
    /// Represents EscapeKeyDownBehavior
    /// </summary>
    public sealed class EscapeKeyDownBehavior {

        #region  Shared Properties

        /// <summary>
        /// Escape key down attached property
        /// </summary>
        public static DependencyProperty EscapeKeyDownProperty = DependencyProperty.RegisterAttached("EscapeKeyDown", typeof(Object), typeof(EscapeKeyDownBehavior), new UIPropertyMetadata(EscapeKeyDownChanged));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the escape key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <returns></returns>
        public static Object GetEscapeKeyDown(DependencyObject depObj) {
            return depObj.GetValue(EscapeKeyDownProperty);
        }

        /// <summary>
        /// Sets the escape key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="value">The value.</param>
        public static void SetEscapeKeyDown(DependencyObject depObj, Object value) {
            depObj.SetValue(EscapeKeyDownProperty, value);
        }

        #endregion

        #region  Methods

        private static void EscapeKeyDownChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e) {

            var uiE = depObj as UIElement;

            if(uiE == null) {
                throw new InvalidOperationException(Resources.EscapeKeyDownBehavior_EscapeKeyDownChanged_This_attached_behavior_only_works_with_UIElements);
            }

            if(e.NewValue != null && e.NewValue is ICommand) {
                uiE.KeyDown += UIElement_EscapeKeyDown;

            } else {
                uiE.KeyDown -= UIElement_EscapeKeyDown;
            }

        }

        static void UIElement_EscapeKeyDown(Object sender, KeyEventArgs e) {

            if(e.Key == Key.Escape) {

                var uiE = (UIElement)sender;
                var cmd = uiE.GetValue(EscapeKeyDownProperty) as ICommand;

                if(cmd != null) {

                    if(sender is Control) {
                        cmd.Execute(((Control)sender).DataContext);

                    } else {
                        cmd.Execute(sender);
                    }

                }
            }
        }

        #endregion
    }
}