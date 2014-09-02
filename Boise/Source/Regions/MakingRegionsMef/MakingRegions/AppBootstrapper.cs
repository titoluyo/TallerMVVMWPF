using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace MakingRegions
{
    internal class AppBootstrapper : MefBootstrapper
    {

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AppBootstrapper).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri(@"ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var behaviorRegistry = base.ConfigureDefaultRegionBehaviors();
            behaviorRegistry.AddIfMissing(MefAutoDiscoveryRegionBehavior.BehaviorKey, typeof(MefAutoDiscoveryRegionBehavior));
            return behaviorRegistry;
        }

        protected override DependencyObject CreateShell()
        {
            var shell = new Shell();
            shell.Show();

            return shell;
        }
    }
}