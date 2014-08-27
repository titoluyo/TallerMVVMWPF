using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Data
{
    interface IDataManager
    {
        ICustomerManager GetCustomerManager();
        IOrderManager GetOrderManager();
    }
}
