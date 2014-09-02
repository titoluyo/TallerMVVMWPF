using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Introduction {

    public partial class DataBindingObjects : UserControl, IRegionMemberLifetime {

        public DataBindingObjects() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
