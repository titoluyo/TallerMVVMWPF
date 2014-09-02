using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Ocean.Wpf.Infrastructure {

    /// <summary>
    /// Represents FocusBindingAssistant
    /// </summary>
    public static class FocusBindingAssistant {

        /// <summary>
        /// Initializes the <see cref="FocusBindingAssistant"/> class.
        /// </summary>
        static FocusBindingAssistant() {}

        /// <summary>
        /// Executes UpdateSource for a TextBox with focus
        /// </summary>
        public static void UpdateFocusedField() {

            var fwE = Keyboard.FocusedElement as FrameworkElement;

            if(fwE == null) return;

            BindingExpression expression = null;

            if(fwE is TextBox) {
                expression = fwE.GetBindingExpression(TextBox.TextProperty);
                //
                //TODO - developers - add more controls types here.  Won't be that many.
                //this would include custom TextBox controls or 3rd party TextBox controls 
            }

            if(expression != null) {
                expression.UpdateSource();
            }
        }
    }
}
