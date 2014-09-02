using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Information.Views;

namespace ThePhoneCompany.Information {

    [ModuleExport(typeof(InformationModule))]
    public class InformationModule : IModule {

        [Import]
        public IRegionManager RegionManager;

        void IModule.Initialize() {
            this.RegionManager.RegisterViewWithRegion(Constants.NavigationRegion, typeof(HomeNavigationItemView));
            this.RegionManager.RegisterViewWithRegion(Constants.NavigationRegion, typeof(AboutNavigationItemView));
        }
    }
}
