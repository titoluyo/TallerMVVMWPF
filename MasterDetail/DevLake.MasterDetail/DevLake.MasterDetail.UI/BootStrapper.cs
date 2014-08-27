using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevLake.MasterDetail.UI.Service;
using DevLake.MasterDetail.UI.Interface;
using DevLake.MasterDetail.UI.View;

namespace DevLake.MasterDetail.UI
{
    class BootStrapper
    {
        public static void Initialize()
        {
            //initialize all the services needed for dependency injection
            //we use the unity framework for dependency injection, but you can choose others
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());  

            //register the IModalDialog using an instance of the CustomerViewDialog
            //this sets up the view
            ServiceProvider.Instance.Register<IModalDialog, CustomerViewDialog>(); 
        }
    }
}
