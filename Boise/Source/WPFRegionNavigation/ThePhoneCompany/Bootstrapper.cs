using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using ThePhoneCompany.Information;
using ThePhoneCompany.Inventory;
using ThePhoneCompany.Views;

namespace ThePhoneCompany {

    public class Bootstrapper : MefBootstrapper {

        protected override void ConfigureAggregateCatalog() {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InformationModule).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(InventoryModule).Assembly));
        }

        protected override DependencyObject CreateShell() {
            return this.Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell() {
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }
    }
}
