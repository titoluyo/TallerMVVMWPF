using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.VisualizingData {

    public partial class ContentControlDemo : UserControl, IRegionMemberLifetime {

        public ContentControlDemo() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
