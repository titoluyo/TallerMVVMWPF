using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace CookMe {
    internal class Bootstrapper : UnityBootstrapper {

        // executes first
        protected override IModuleCatalog CreateModuleCatalog() {
            // load all modules listed in the app.config file
            return new ConfigurationModuleCatalog();
        }

        //// executes second
        //protected override void ConfigureContainer() {
        //    base.ConfigureContainer();

        //    // load infrastructure type
        //    //this.Container.RegisterType(typeof(IEventResolver<>), typeof(EventResolver<>));

        //    // register data singleton
        //    //this.Container.RegisterType(typeof(Lessons), new ContainerControlledLifetimeManager());
        //}

        // executes third
        protected override DependencyObject CreateShell() {
            return this.Container.Resolve<ShellView>();
        }

        // executes fourth
        protected override void InitializeShell() {
            Application.Current.MainWindow = (ShellView)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
