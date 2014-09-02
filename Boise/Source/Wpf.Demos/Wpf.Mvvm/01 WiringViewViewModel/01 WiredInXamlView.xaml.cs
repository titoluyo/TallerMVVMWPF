using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.Mvvm.WiringViewViewModel {

    [RegionMemberLifetime(KeepAlive=false)]
    public partial class WiredInXamlView : UserControl {

        public WiredInXamlView() {
            InitializeComponent();
        }
    }
}
