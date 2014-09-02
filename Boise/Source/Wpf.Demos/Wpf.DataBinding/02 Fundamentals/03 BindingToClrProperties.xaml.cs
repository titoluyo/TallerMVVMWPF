using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.DataBinding.Data;

namespace Wpf.DataBinding.Fundamentals {

    public partial class BindingToClrProperties : UserControl, IRegionMemberLifetime {
        public BindingToClrProperties() {
            InitializeComponent();
            this.Loaded += (s, e) => { this.DataContext = PeopleService.Person; };
        }

        void btnChangeFirstName_Click(Object sender, RoutedEventArgs e) {
            PeopleService.Person.FirstName = "CHANGED";
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
