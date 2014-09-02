using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wpf.Common.Data;
using Wpf.Common.Model;
using Wpf.Validation.Rules;

namespace Wpf.Validation {
    public class ValidationModule : IModule {

        readonly IUnityContainer _container;
        readonly Lessons _lessons;

        public ValidationModule(IUnityContainer container, Lessons lessons) {
            if (container == null) throw new ArgumentNullException("container");
            if (lessons == null) throw new ArgumentNullException("lessons");
            _container = container;
            _lessons = lessons;
        }

        void IModule.Initialize() {
            // validation lessons
            _container.RegisterType(typeof(Object), typeof(ContactView), typeof(ContactView).FullName);
            _container.RegisterType(typeof(Object), typeof(UsingWpfValidationRules), typeof(UsingWpfValidationRules).FullName);
            
            _lessons.Add(new Lesson { Section = "Validation", Title = "IDataErrorInfo & Type Mismatch Exceptions", View = typeof(ContactView).FullName });
            _lessons.Add(new Lesson { Section = "Validation", Title = "Using WPF Validation Rules", View = typeof(UsingWpfValidationRules).FullName });
        }
    }
}


