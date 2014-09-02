using System;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Regions;

namespace ThePhoneCompany.Views {

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class Shell : Window, IPartImportsSatisfiedNotification {

        [Import]
        public IRegionManager RegionManager { get; set; }

        public Shell() {
            InitializeComponent();
        }

        void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            if(e.Action == NotifyCollectionChangedAction.Add) {
                var region = e.NewItems[e.NewStartingIndex] as Region;
                if(region != null) {
                    region.NavigationService.NavigationFailed += NavigationService_NavigationFailed;
                }
            }
        }

        void NavigationService_NavigationFailed(object sender, RegionNavigationFailedEventArgs e) {
            MessageBox.Show(e.Error.ToString());
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            this.RegionManager.Regions.CollectionChanged += Regions_CollectionChanged;
        }
    }
}
