using System;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.DataBinding.Data;

namespace Wpf.DataBinding.Introduction {

    public partial class DataContextSetFromAnotherControlSourceInCode : UserControl, IRegionMemberLifetime {

        public DataContextSetFromAnotherControlSourceInCode() {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = PeopleService.People; };
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
