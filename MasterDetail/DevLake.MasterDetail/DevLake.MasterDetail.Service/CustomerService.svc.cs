using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DevLake.MasterDetail.Business;

namespace DevLake.MasterDetail.Service
{
    public class CustomerService : ICustomerService
    {

        List<Customer> ICustomerService.GetCustomers()
        {
            List<Customer> result = new List<Customer>();
            foreach (Business.Customer i in Business.CustomerManager.Instance().CustomerList)
                result.Add(new Customer(i));
            return result;
        }

        int ICustomerService.AddCustomer(string firstName, string lastName)
        {
            return Business.CustomerManager.Instance().AddCustomer(firstName, lastName);
        }

        void ICustomerService.UpdateCustomer(Customer c)
        {
            Business.CustomerManager.Instance().UpdateCustomer(new Business.Customer(c.CustomerId, c.FirstName, c.LastName));
        }

        void ICustomerService.DeleteCustomer(int customerId)
        {
            Business.CustomerManager.Instance().DeleteCustomer(customerId);
        }


    }
}
