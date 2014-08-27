using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Data
{
    public interface IOrderManager
    {
        int Add(string description, int quantity, int customerId);
        void Delete(int orderId);
        void Update(int orderId, string description, int quantity);
        List<Order> FindByCustomer(int customerId);
        Order Find(int orderId);        
    }
}
