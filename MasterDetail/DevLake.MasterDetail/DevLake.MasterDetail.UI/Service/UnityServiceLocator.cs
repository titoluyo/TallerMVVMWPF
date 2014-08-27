using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevLake.MasterDetail.UI.Interface;
using Microsoft.Practices.Unity;

namespace DevLake.MasterDetail.UI.Service
{
    class UnityServiceLocator : IServiceLocator
    {
        private UnityContainer container;

        public UnityServiceLocator()
        {
            container = new UnityContainer();
        }
    
        void IServiceLocator.Register<TInterface, TImplementation>()
        {
            container.RegisterType<TInterface, TImplementation>();
        }

        TInterface IServiceLocator.Get<TInterface>()
        {
            return container.Resolve<TInterface>();
        }
    }
}
