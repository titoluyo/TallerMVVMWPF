using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.DataBinding.Collections {

    public partial class DataTemplateSelectorDemo : UserControl, IRegionMemberLifetime {
    
        public DataTemplateSelectorDemo() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
