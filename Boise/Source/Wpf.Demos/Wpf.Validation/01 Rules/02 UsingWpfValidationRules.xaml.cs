using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace Wpf.Validation.Rules {

    public partial class UsingWpfValidationRules : UserControl, IRegionMemberLifetime {

        public UsingWpfValidationRules() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
