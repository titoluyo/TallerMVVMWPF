using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.VisualizingData {

    public partial class ItemsControlDemo : UserControl, IRegionMemberLifetime {
    
        public ItemsControlDemo() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
