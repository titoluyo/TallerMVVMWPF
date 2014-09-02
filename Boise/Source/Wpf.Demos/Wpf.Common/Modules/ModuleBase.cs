using System;
using System.Reflection;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wpf.Common.Unity;

namespace Wpf.Common.Modules {
    /// <summary>
    /// Represents the ModuleBase class.  Provides automatic container loading for view - view model wiring
    /// </summary>
    public abstract class ModuleBase : IModule {

        readonly IUnityContainer _container;

        protected IUnityContainer Container {
            get { return _container; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleBase"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected ModuleBase(IUnityContainer container) {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
        }

        /// <summary>
        /// Provides automatic container loading for view - view model wiring.
        /// </summary>
        public virtual void Initialize() {
            var asy = Assembly.GetCallingAssembly();
            ContainerLoader.InitalizeLoadViews(_container, asy);
        }
    }
}
