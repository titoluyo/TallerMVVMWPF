using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Fundamentals {

    public partial class BindingModes : UserControl, IRegionMemberLifetime {

        public BindingModes() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
