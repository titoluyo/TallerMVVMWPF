using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevLake.MasterDetail.UI.Interface;

namespace DevLake.MasterDetail.UI.Service
{
    class ServiceProvider
    {
        public static IServiceLocator Instance { get; private set; }

        public static void RegisterServiceLocator(IServiceLocator s)
        {
            Instance = s;
        }
    }
}
