using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.Common.Unity;

namespace Wpf.Mvvm.WiringViewViewModel {

    [RegionMemberLifetime(KeepAlive = false)]
    [ViewContainerInitializer(typeof(ViewModel))]
    public partial class WiredByUnityView : UserControl {
        
        public WiredByUnityView() {
            InitializeComponent();
        }
    }
}
