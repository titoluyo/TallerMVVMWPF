using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wpf.Common.Data;
using Wpf.Common.Model;
using Wpf.DataBinding.Collections;
using Wpf.DataBinding.Fundamentals;
using Wpf.DataBinding.Introduction;
using Wpf.DataBinding.VisualizingData;

namespace Wpf.DataBinding {
    public class DataBindingModule : IModule {

        readonly IUnityContainer _container;
        readonly Lessons _lessons;

        public DataBindingModule(IUnityContainer container, Lessons lessons) {
            if (container == null) throw new ArgumentNullException("container");
            if (lessons == null) throw new ArgumentNullException("lessons");
            _container = container;
            _lessons = lessons;
        }

        void IModule.Initialize() {

            // introduction lessons
            _container.RegisterType(typeof(Object), typeof(SimpleDataBinding), typeof(SimpleDataBinding).FullName);
            _container.RegisterType(typeof(Object), typeof(DataBindingObjects), typeof(DataBindingObjects).FullName);
            _container.RegisterType(typeof(Object), typeof(DataContent), typeof(DataContent).FullName);
            _container.RegisterType(typeof(Object), typeof(DataContextSetInCode), typeof(DataContextSetInCode).FullName);
            _container.RegisterType(typeof(Object), typeof(DataContextSetFromAnotherControl), typeof(DataContextSetFromAnotherControl).FullName);
            _container.RegisterType(typeof(Object), typeof(DataContextSetFromAnotherControlSourceInCode), typeof(DataContextSetFromAnotherControlSourceInCode).FullName);
            _container.RegisterType(typeof(Object), typeof(BindingsSetInCode), typeof(BindingsSetInCode).FullName);

            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Simple Data Binding", View = typeof(SimpleDataBinding).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Binding Objects", View = typeof(DataBindingObjects).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Context", View = typeof(DataContent).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Context Set In Code", View = typeof(DataContextSetInCode).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Context Set From Another Control", View = typeof(DataContextSetFromAnotherControl).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Context Set From Another Control v2", View = typeof(DataContextSetFromAnotherControlSourceInCode).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Introduction", Title = "Data Bindings Set In Code", View = typeof(BindingsSetInCode).FullName });

            
            // fundamentals lessons
            _container.RegisterType(typeof(Object), typeof(BindingComponents), typeof(BindingComponents).FullName);
            _container.RegisterType(typeof(Object), typeof(RelativeSource), typeof(RelativeSource).FullName);
            _container.RegisterType(typeof(Object), typeof(BindingToClrProperties), typeof(BindingToClrProperties).FullName);
            _container.RegisterType(typeof(Object), typeof(BindingModes), typeof(BindingModes).FullName);
            _container.RegisterType(typeof(Object), typeof(UpdateSourceTrigger), typeof(UpdateSourceTrigger).FullName);

            _lessons.Add(new Lesson { Section = "Data Binding Fundamentals", Title = "Binding Componets", View = typeof(BindingComponents).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Fundamentals", Title = "Relative Source", View = typeof(RelativeSource).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Fundamentals", Title = "Binding To CLR Properties", View = typeof(BindingToClrProperties).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Fundamentals", Title = "Binding Modes", View = typeof(BindingModes).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Fundamentals", Title = "Update Source Trigger", View = typeof(UpdateSourceTrigger).FullName });


            // visualizing data lessons
            _container.RegisterType(typeof(Object), typeof(ContentControlDemo), typeof(ContentControlDemo).FullName);
            _container.RegisterType(typeof(Object), typeof(ItemsControlDemo), typeof(ItemsControlDemo).FullName);

            _lessons.Add(new Lesson { Section = "Data Binding Visualizing Data", Title = "Content Control", View = typeof(ContentControlDemo).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Visualizing Data", Title = "Items Control", View = typeof(ItemsControlDemo).FullName });


            // collections lessons
            _container.RegisterType(typeof(Object), typeof(BindingToCollections), typeof(BindingToCollections).FullName);
            _container.RegisterType(typeof(Object), typeof(CollectionViews), typeof(CollectionViews).FullName);
            _container.RegisterType(typeof(Object), typeof(Sorting), typeof(Sorting).FullName);
            _container.RegisterType(typeof(Object), typeof(Grouping), typeof(Grouping).FullName);
            _container.RegisterType(typeof(Object), typeof(Converters), typeof(Converters).FullName);
            _container.RegisterType(typeof(Object), typeof(DataTemplateSelectorDemo), typeof(DataTemplateSelectorDemo).FullName);

            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Binding to Collections", View = typeof(BindingToCollections).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Collection Views", View = typeof(CollectionViews).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Sorting", View = typeof(Sorting).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Grouping", View = typeof(Grouping).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Converters", View = typeof(Converters).FullName });
            _lessons.Add(new Lesson { Section = "Data Binding Collections", Title = "Data Template Selector", View = typeof(DataTemplateSelectorDemo).FullName });
        }
    }
}
