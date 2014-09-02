using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Introduction {

    public partial class DataContextSetFromAnotherControl : UserControl, IRegionMemberLifetime {

        public DataContextSetFromAnotherControl() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
