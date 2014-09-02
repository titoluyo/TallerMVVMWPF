using System.ComponentModel.Composition;
using Common;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA
{
    [ModuleExport("ModuleA", typeof (ModuleInit))]
    internal class ModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => new AreaA(_regionManager));
            //_regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => new AreaB(_regionManager));
        }

        #endregion
    }
}