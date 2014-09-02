using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Inventory.Views;

namespace ThePhoneCompany.Inventory {

    [ModuleExport(typeof(InventoryModule))]
    public class InventoryModule : IModule {

        [Import]
        public IRegionManager RegionManager;

        void IModule.Initialize() {
            this.RegionManager.RegisterViewWithRegion(Constants.NavigationRegion, typeof(InventoryNavigationItemView));
        }
    }
}
