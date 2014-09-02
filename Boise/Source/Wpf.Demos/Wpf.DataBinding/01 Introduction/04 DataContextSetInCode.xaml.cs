using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.DataBinding.Data;

namespace Wpf.DataBinding.Introduction {

    public partial class DataContextSetInCode : UserControl, IRegionMemberLifetime {
        
        public DataContextSetInCode() {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = PeopleService.Person; };
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
