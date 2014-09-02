using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.UnityExtensions;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Modularity;
using CmdletConsole.Math;

namespace CmdletConsole
{    
    public class MainBootstrapper : UnityBootstrapper
    {                
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        // TODO: 03. Add a reference to the assembly and directly add the type to the module catalog.

        protected override void ConfigureModuleCatalog()
        {            
            Type moduleAType = typeof(MathModule);
            ModuleCatalog.AddModule(new ModuleInfo(moduleAType.Name, moduleAType.AssemblyQualifiedName));
        }

        // TODO: 04. Let's step into the Run method

        public override void Run(bool runWithDefaultConfiguration)
        {
            base.Run(runWithDefaultConfiguration);
        }

        // TODO: 05. Let's step into the InitializeModules method

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        // TODO: 06. Let's look at ModuleCatalog (Ctrl M + Ctrl O)
    }
}
