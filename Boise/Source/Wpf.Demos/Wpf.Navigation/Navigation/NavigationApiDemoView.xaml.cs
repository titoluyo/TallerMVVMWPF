using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Wpf.Navigation.Navigation {

    public partial class NavigationApiDemoView : UserControl {

        [Dependency]
        public NavigationApiDemoViewModel NavigationApiDemoViewModel {
            get { return this.DataContext as NavigationApiDemoViewModel; }
            set { this.DataContext = value; }
        }

        public NavigationApiDemoView() {
            InitializeComponent();
        }
    }
}
