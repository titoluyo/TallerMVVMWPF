using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Business
{
    public class Order
    {
        private string description = null;
        private int? quantity = null;
        private Customer customer = null;

        public int OrderId 
        { 
            get; 
            private set; 
        }

        public string Description 
        {
            get
            {
                if (description == null)
                    Refresh();
                return description;
            }
        }   

        public int Quantity 
        {
            get
            {
                if (quantity == null)
                    Refresh();
                return (int)quantity;
            }
        }

        public Customer Customer
        {
            get
            {
                if (customer == null)
                    GetCustomer();
                return customer;
            }
        }

        private Order() { }

        public Order(int orderId)
        {
            this.OrderId = orderId;
        }

        public Order(int orderId, string description, int quantity)
        {
            this.OrderId = orderId;
            this.description = description;
            this.quantity = quantity;
        }

        public void Refresh()
        {
            Data.FactoryManager m = new Data.FactoryManager();
            Data.Order i = m.GetOrderManager().Find(this.OrderId);
            this.description = i.Description;
            this.quantity = i.Quantity;
        }

        private void GetCustomer()
        {
            Data.FactoryManager m = new Data.FactoryManager();
            Data.Customer i = m.GetCustomerManager().FindByOrder(this.OrderId);
            customer = new Customer(i.CustomerId, i.FirstName, i.LastName);
        }        
    }
}
