using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Introduction {

    public partial class SimpleDataBinding : UserControl, IRegionMemberLifetime {
        public SimpleDataBinding() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
