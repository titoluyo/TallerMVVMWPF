using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data = DevLake.MasterDetail.Data;

namespace DevLake.MasterDetail.Business
{
    /// <summary>
    /// Singleton class that manages all the customers
    /// </summary>
    public class CustomerManager
    {
        private static CustomerManager instance = null;

        public List<Customer> CustomerList
        {
            get { return GetCustomerList(); }
        }

        private CustomerManager() {}

        public static CustomerManager Instance()
        {
            if (instance == null)
                instance = new CustomerManager();
            return instance;
        }
       
        public Customer GetCustomer(int customerId)
        {
            return CustomerList.Where(i => i.CustomerId == customerId).First();
        }

        public int AddCustomer(string firstName, string lastName)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            return m.GetCustomerManager().Add(firstName, lastName);
        }

        public void UpdateCustomer(Customer c)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            m.GetCustomerManager().Update(c.CustomerId, c.FirstName, c.LastName);
        }

        public void DeleteCustomer(int customerId)
        {
            Data.FactoryManager m = new Data.FactoryManager();
            m.GetCustomerManager().Delete(customerId);
        }

        private List<Customer> GetCustomerList()
        {
            List<Customer> customerList = new List<Customer>();
            Data.FactoryManager m = new Data.FactoryManager();
            foreach (Data.Customer i in m.GetCustomerManager().FindAll())
            {
                Customer c = new Customer(i.CustomerId, i.FirstName, i.LastName);
                customerList.Add(c);
            }
            return customerList;
        }

        
    }
}
