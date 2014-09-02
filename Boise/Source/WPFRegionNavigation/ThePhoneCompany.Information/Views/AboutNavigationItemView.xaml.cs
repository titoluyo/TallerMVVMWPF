using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Constants;

namespace ThePhoneCompany.Information.Views {

    [Export]
    [ViewSortHint(Constants.AboutSortHint)]
    public partial class AboutNavigationItemView : UserControl {

        [Import]
        public IRegionManager RegionManager;

        public AboutNavigationItemView() {
            InitializeComponent();
        }

        void btnNavigate_Click(Object sender, RoutedEventArgs e) {
            this.RegionManager.Regions[Constants.MainContentRegion].RequestNavigate(typeof(AboutView).FullName);
        }
    }
}
