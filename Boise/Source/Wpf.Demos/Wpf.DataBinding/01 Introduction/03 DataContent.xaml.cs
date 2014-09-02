using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Introduction {

    public partial class DataContent : UserControl, IRegionMemberLifetime {

        public DataContent() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
