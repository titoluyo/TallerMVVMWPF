using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace Wpf.Presentation.Views {

    public partial class NavigationView : UserControl {

        [Dependency]
        public NavigationViewModel NavigationViewModel {
            get { return this.DataContext as NavigationViewModel; }
            set { this.DataContext = value; }
        }

        public NavigationView() {
            InitializeComponent();
        }
    }
}
