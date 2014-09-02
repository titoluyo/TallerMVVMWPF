using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ocean.Wpf.Properties;

namespace Ocean.Wpf.Behaviors {

    /// <summary>
    /// Represents EnterKeyDownBehavior
    /// </summary>
    public sealed class EnterKeyDownBehavior {

        #region  Shared Properties

        /// <summary>
        /// Enter key down attached property
        /// </summary>
        public static DependencyProperty EnterKeyDownProperty = DependencyProperty.RegisterAttached("EnterKeyDown", typeof(Object), typeof(EnterKeyDownBehavior), new UIPropertyMetadata(EnterKeyDownChanged));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the enter key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <returns></returns>
        public static Object GetEnterKeyDown(DependencyObject depObj) {
            return depObj.GetValue(EnterKeyDownProperty);
        }

        /// <summary>
        /// Sets the enter key down.
        /// </summary>
        /// <param name="depObj">The dep obj.</param>
        /// <param name="value">The value.</param>
        public static void SetEnterKeyDown(DependencyObject depObj, Object value) {
            depObj.SetValue(EnterKeyDownProperty, value);
        }

        #endregion

        #region  Methods

        static void EnterKeyDownChanged(DependencyObject depObj, DependencyPropertyChangedEventArgs e) {

            var uiE = depObj as UIElement;

            if(uiE == null) {
                throw new InvalidOperationException(Resources.EnterKeyDownBehavior_EnterKeyDownChanged_This_attached_behavior_only_works_with_UIElements);
            }

            if(e.NewValue != null && e.NewValue is ICommand) {
                uiE.KeyDown += UIElement_EnterKeyDown;

            } else {
                uiE.KeyDown -= UIElement_EnterKeyDown;
            }

        }

        static void UIElement_EnterKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            
            var uiE = (UIElement)sender;
            var cmd = uiE.GetValue(EnterKeyDownProperty) as ICommand;

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