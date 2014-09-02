
using System;
using System.Windows;
using System.Windows.Controls;
using Stuff.Services.Container;
using Stuff.UI;

namespace Stuff {

    public partial class App : Application {
        public App() {
            Startup += new StartupEventHandler(App_Startup);
            DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
            MessageBox.Show(e.ToString(), "Unhandled Exception - Application Closing", MessageBoxButton.OK, MessageBoxImage.Stop);
            Application.Current.Shutdown();
        }

        void App_Startup(object sender, StartupEventArgs e) {
            //HACK - how to force all TextBoxes to select their text on focus
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus), true);
            ServiceLoader.LoadRunTimeServices();
            var w = new Shell();
            w.Show();
        }

        void TextBox_GotFocus(Object sender, RoutedEventArgs e) {
            ((TextBox)sender).SelectAll();
        }
    }
}
