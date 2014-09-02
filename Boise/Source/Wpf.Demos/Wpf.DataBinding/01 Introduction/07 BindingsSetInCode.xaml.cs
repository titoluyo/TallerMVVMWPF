using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Prism.Regions;
using Wpf.DataBinding.Data;

namespace Wpf.DataBinding.Introduction {

    public partial class BindingsSetInCode : UserControl, IRegionMemberLifetime {

        public BindingsSetInCode() {
            InitializeComponent();
            this.Loaded += BindingsSetInCode_Loaded;
        }

        void BindingsSetInCode_Loaded(object sender, System.Windows.RoutedEventArgs e) {

            this.txtFirstName.SetBinding(TextBox.TextProperty, new Binding("FirstName"));

            var b = new Binding();
            b.Path = new PropertyPath("LastName");
            this.txtLastName.SetBinding(TextBox.TextProperty, b);

            b = new Binding();
            b.Path = new PropertyPath("Birthday");
            b.StringFormat = "d";
            b.TargetNullValue = String.Empty;
            this.txtBirthday.SetBinding(TextBox.TextProperty, b);

            this.DataContext = PeopleService.People;
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }
    }
}
