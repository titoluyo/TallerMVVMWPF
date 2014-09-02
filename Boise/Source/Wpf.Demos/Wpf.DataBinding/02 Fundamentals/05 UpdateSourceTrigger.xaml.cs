using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.DataBinding.Data;

namespace Wpf.DataBinding.Fundamentals {

    public partial class UpdateSourceTrigger : UserControl, IRegionMemberLifetime {

        public UpdateSourceTrigger() {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = PeopleService.Person; };
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
