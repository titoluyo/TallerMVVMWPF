using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.ServiceLocation;

namespace CmdletConsole
{
    // TODO: 01. Derive from bootstrapper and hook up the main window through the container.

    public class MainBootstrapper : UnityBootstrapper
    {                
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
            //return App.Current.MainWindow;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
