using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Wpf.Mvvm.WiringViewViewModel {

    [RegionMemberLifetime(KeepAlive = false)]
    public partial class WiredUsingPropertyInjection : UserControl {

        [Dependency]
        public ViewModel ViewModel {
            get { return this.DataContext as ViewModel; }
            set { this.DataContext = value; }
        }

        public WiredUsingPropertyInjection() {
            InitializeComponent();
        }
    }
}
