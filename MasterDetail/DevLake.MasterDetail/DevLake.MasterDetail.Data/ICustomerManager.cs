using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Data
{
    public interface ICustomerManager
    {        
        int Add(string firstName, string lastName);
        void Delete(int customerId);
        void Update(int customerId, string firstName, string lastName);
        List<Customer> FindAll();
        Customer FindByOrder(int orderId);
    }
}
