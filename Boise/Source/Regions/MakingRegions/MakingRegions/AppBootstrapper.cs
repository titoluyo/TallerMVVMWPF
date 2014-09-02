using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;

namespace MakingRegions
{
    internal class AppBootstrapper : UnityBootstrapper
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                new Uri(@"ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override DependencyObject CreateShell()
        {
            var shell = new Shell();
            shell.Show();

            return shell;
        }
    }
}