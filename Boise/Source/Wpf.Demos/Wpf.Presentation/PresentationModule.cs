using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Wpf.Common.Data;
using Wpf.Common.Infrastructure;
using Wpf.Common.Model;
using Wpf.Presentation.Views;

namespace Wpf.Presentation {
    public class PresentationModule : IModule {

        private readonly Lessons _lessons;
        readonly IUnityContainer _container;
        readonly IRegionManager _regionManager;

        public PresentationModule(Lessons lessons, IUnityContainer container, IRegionManager regionManager) {
            if (lessons == null) throw new ArgumentNullException("lessons");
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            _lessons = lessons;
            _container = container;
            _regionManager = regionManager;
        }

        void IModule.Initialize() {
            // application views
            _container.RegisterType(typeof(Object), typeof(NavigationView), typeof(NavigationView).FullName);
            _container.RegisterType(typeof(Object), typeof(HomeView), typeof(HomeView).FullName);

            _lessons.Insert(0, new Lesson { Section = "Welcome", Title = "Home", View = typeof(HomeView).FullName });

            // load navigation region using Prism View Discovery
            _regionManager.RegisterViewWithRegion(Constants.NavigationRegionName, () => _container.Resolve<NavigationView>());
            
        }
    }
}
