using System;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Wpf.Commands.RoutedCommands;
using Wpf.Common.Data;
using Wpf.Common.Model;

namespace Wpf.Commands {

    public class CommandsModule : IModule {

        readonly IUnityContainer _container;
        readonly Lessons _lessons;

        public CommandsModule(IUnityContainer container, Lessons lessons) {
            if (container == null) throw new ArgumentNullException("container");
            if (lessons == null) throw new ArgumentNullException("lessons");
            _container = container;
            _lessons = lessons;
        }

        void IModule.Initialize() {
            // validation lessons
            _container.RegisterType(typeof(Object), typeof(RoutedCommandView), typeof(RoutedCommandView).FullName);

            _lessons.Add(new Lesson { Section = "Commands in MVVM", Title = "Routed Commands", View = typeof(RoutedCommandView).FullName });
        }
    }
}


