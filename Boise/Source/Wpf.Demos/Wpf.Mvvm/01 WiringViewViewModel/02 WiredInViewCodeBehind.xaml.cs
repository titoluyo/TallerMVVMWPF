using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.Mvvm.WiringViewViewModel {

    [RegionMemberLifetime(KeepAlive = false)]
    public partial class WiredInViewCodeBehind : UserControl {

        public WiredInViewCodeBehind() {
            InitializeComponent();
            this.Loaded += (s, e) => this.DataContext = new ViewModel();
        }
    }
}
