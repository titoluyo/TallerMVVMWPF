using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Constants;

namespace ThePhoneCompany.Information.Views {
    
    [Export]
    [ViewSortHint(Constants.HomeSortHint)]
    public partial class HomeNavigationItemView : UserControl {

        [Import]
        public IRegionManager RegionManager;

        public HomeNavigationItemView() {
            InitializeComponent();
            this.Loaded += (s, e) => Navigate();
        }

        void btnNavigate_Click(Object sender, RoutedEventArgs e) {
            Navigate();
        }

        void Navigate() {
            this.RegionManager.Regions[Constants.MainContentRegion].RequestNavigate(typeof(HomeView).FullName);
        }
    }
}
