using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEncuestas.Interface;

namespace WpfEncuestas.Sevice
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
