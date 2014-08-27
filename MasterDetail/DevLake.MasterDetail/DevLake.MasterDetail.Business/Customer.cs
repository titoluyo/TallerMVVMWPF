using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Business
{
    public class Customer
    {
        public int CustomerId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public List<Order> Orders
        {
            get { return GetOrders(); }
        } 

        private Customer() { }

        public Customer(int customerId, string firstName, string lastName)
        {
            this.CustomerId = customerId;
            this.FirstName = firstName;
            this.LastName = lastName;
        }               

        public int AddOrder(string orderDescription, int quantity)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            DateTime orderDate = DateTime.Now;
            return m.GetOrderManager().Add(orderDescription, quantity, this.CustomerId);
        }

        public void DeleteOrder(int orderId)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            m.GetOrderManager().Delete(orderId);
        }

        public void UpdateOrder(Order i)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            m.GetOrderManager().Update(i.OrderId, i.Description, i.Quantity);
        }

        private List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            Data.FactoryManager m = new Data.FactoryManager();
            foreach (Data.Order i in m.GetOrderManager().FindByCustomer(this.CustomerId))
                orders.Add(new Order(i.OrderId, i.Description, i.Quantity));
            return orders;
        }

    }
}
