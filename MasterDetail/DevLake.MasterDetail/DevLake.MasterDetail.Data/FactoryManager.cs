using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Data
{
    public class FactoryManager
    {
        private IDataManager dataManager;

        public FactoryManager()
        { 
            //can easily switch to other providers in the future without changing client code
            dataManager = new EntityFrameworkManager();
        }

        public ICustomerManager GetCustomerManager()
        {
            return dataManager.GetCustomerManager();
        }

        public IOrderManager GetOrderManager()
        {
            return dataManager.GetOrderManager();
        }
    }
}
