using System;
using Microsoft.Practices.Unity;
using Wpf.Common.Data;
using Wpf.Common.Model;
using Wpf.Common.Modules;
using Wpf.Mvvm.WiringViewViewModel;

namespace Wpf.Mvvm {

    public class MvvmModule : ModuleBase {

        readonly IUnityContainer _container;
        readonly Lessons _lessons;

        public MvvmModule(IUnityContainer container, Lessons lessons)
            : base(container) {
            if (lessons == null) throw new ArgumentNullException("lessons");
            _container = container;
            _lessons = lessons;
        }

        public override void Initialize() {
            // all views in the assembly decorated with 
            //   ViewContainerInitializerAttribute or
            //   ViewModelContainerInitializerAttribute.cs 
            // are loaded into the container in base.Initialize
            base.Initialize();

            // validation lessons
            _container.RegisterType(typeof(Object), typeof(WiredInXamlView), typeof(WiredInXamlView).FullName);
            _container.RegisterType(typeof(Object), typeof(WiredInViewCodeBehind), typeof(WiredInViewCodeBehind).FullName);
            _container.RegisterType(typeof(Object), typeof(WiredUsingPropertyInjection), typeof(WiredUsingPropertyInjection).FullName);

            _lessons.Add(new Lesson { Section = "Model-View-ViewModel", Title = "Wired in XAML", View = typeof(WiredInXamlView).FullName });
            _lessons.Add(new Lesson { Section = "Model-View-ViewModel", Title = "Wired in View Code Behind", View = typeof(WiredInViewCodeBehind).FullName });
            _lessons.Add(new Lesson { Section = "Model-View-ViewModel", Title = "Wired Using Property Injection", View = typeof(WiredUsingPropertyInjection).FullName });


            // NOTICE: These are not added to the container using the above syntax.
            //         They are registered in the base.Initialize() method.
            _lessons.Add(new Lesson { Section = "Model-View-ViewModel", Title = "Wired by Unity", View = typeof(WiredByUnityView).FullName });
            _lessons.Add(new Lesson { Section = "Model-View-ViewModel", Title = "Wired by Resouce Lookup", View = typeof(WiredByResourceLookupView).FullName });
        }
    }
}


