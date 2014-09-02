using Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA
{
    internal class ModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof (AreaA));
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof (AreaB));
        }

        #endregion
    }
}