using System.Windows;

namespace Wpf.Demonstrations {

    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
