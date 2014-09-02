using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace CmdletConsole
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //TODO: 02. Remove StartupUri="MainWindow.xaml" from App.xaml, and run bootstrapper.

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);            

            MainBootstrapper bootstrapper = new MainBootstrapper();
            bootstrapper.Run();
        }
    }
}
