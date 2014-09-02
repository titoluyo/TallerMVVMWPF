using System.Windows;

namespace MakingRegions
{
    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var bootstrapper = new AppBootstrapper();
            bootstrapper.Run();

            base.OnStartup(e);
        }
    }
}