using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace ModuleB
{
    class ModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            _regionManager.AddToRegion(RegionNames.ContentRegion, new View2());
        }

        #endregion
    }
}
