using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Commands;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Common.Infrastructure;
using ThePhoneCompany.Inventory.Events;

namespace ThePhoneCompany.Inventory.Views {

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class InventoryNavigationItemViewModel : ViewModelBase {

        String _buttonText = Constants.Inventory;

        public IRegionManager RegionManager { get; set; }

        public String ButtonText {
            get { return _buttonText; }
            set {
                _buttonText = value;
                this.RaisePropertyChanged("ButtonText");
            }
        }

        public ICommand NavigateCommand {
            get { return new RelayCommand(() => this.RegionManager.Regions[Constants.MainContentRegion].RequestNavigate(typeof(InventoryView).FullName)); }
        }

        [ImportingConstructor]
        public InventoryNavigationItemViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) {
            this.RegionManager = regionManager;

            InventoryNavigatedEvent inventoryNavigatedEvent = eventAggregator.GetEvent<InventoryNavigatedEvent>();
            inventoryNavigatedEvent.Subscribe(this.SetButtonText);

        }

        void SetButtonText(String uriString) {
            this.Dispatcher.BeginInvoke((Action)delegate {
                this.ButtonText = MakeLabelWithCountForApplicationSuite(Constants.Inventory, Constants.Inventory);
            });
        }

        String MakeLabelWithCountForApplicationSuite(String applicaitonName, String labelText) {
            Int32 count = 0;

            foreach(var item in this.RegionManager.Regions[Constants.MainContentRegion].Views) {
                var fwe = item as FrameworkElement;
                if (fwe == null) continue;
                var nonLinearNavigationObject = fwe.DataContext as INonLinearNavigationObject;
                if(nonLinearNavigationObject != null && nonLinearNavigationObject.Application == applicaitonName) {
                    count += 1;
                }
            }

            return count == 0 ? labelText : String.Format("{0}-({1})", labelText, count);
        }
    }
}
