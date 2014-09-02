using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using WPFPrismv4Navigation.UI;

namespace WPFPrismv4Navigation {

    public class Bootstrapper : MefBootstrapper {

        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();
            var catalog = new AssemblyCatalog(typeof(Bootstrapper).Assembly);
            this.AggregateCatalog.Catalogs.Add(catalog);
        }

        protected override DependencyObject CreateShell() {
            return this.Container.GetExportedValue<ShellView>();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow = (ShellView)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
