using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.Behaviors {

    /// <summary>
    /// Represents KeyDownBehavior
    /// </summary>
    public sealed class KeyDownBehavior {

        #region  Shared Properties

        /// <summary>
        /// Key down
        /// </summary>
        public static DependencyProperty KeyDownProperty = DependencyProperty.RegisterAttached("KeyDown", typeof(Object), typeof(KeyDownBehavior), new UIPropertyMetadata(KeyDownChanged));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <returns></returns>
        public static object GetKeyDown(DependencyObject depObj) {
            return depObj.GetValue(KeyDownProperty);
        }

        /// <summary>
        /// Sets the key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="value">The value.</param>
        public static void SetKeyDown(DependencyObject depObj, object value) {
            depObj.SetValue(KeyDownProperty, value);
        }

        #endregion

        #region  Methods

        private static void KeyDownChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e) {

            var uiE = depObj as UIElement;

            if(uiE == null) {
                throw new InvalidOperationException(Resources.KeyDownBehavior_KeyDownChanged_This_attached_behavior_only_works_with_UIElements);
            }

            if(e.NewValue != null && e.NewValue is ICommand) {
                uiE.KeyDown += UIElement_KeyDown;

            } else {
                uiE.KeyDown -= UIElement_KeyDown;
            }

        }

        static void UIElement_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            
            var uiE = (UIElement)sender;
            var cmd = uiE.GetValue(KeyDownProperty) as ICommand;

            if (cmd == null) return;
            
            if(e.OriginalSource is Control) {
                cmd.Execute(((Control)e.OriginalSource).DataContext);
            } else {
                cmd.Execute(sender);
            }
        }

        #endregion
    }
}