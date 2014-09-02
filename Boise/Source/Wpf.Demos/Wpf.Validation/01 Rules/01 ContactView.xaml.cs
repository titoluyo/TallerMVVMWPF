using System;
using Microsoft.Practices.Prism.Regions;
using Wpf.Validation.Infrastructure;

namespace Wpf.Validation.Rules {

    public partial class ContactView : MaintenanceFormViewBase, IRegionMemberLifetime {

        public ContactView() {
            InitializeComponent();
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
