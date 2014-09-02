using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Collections {

    public partial class CollectionViews : UserControl, IRegionMemberLifetime {

        public CollectionViews() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
