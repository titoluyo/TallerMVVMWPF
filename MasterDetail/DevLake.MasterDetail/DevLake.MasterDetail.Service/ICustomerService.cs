using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DevLake.MasterDetail.Service
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        List<Customer> GetCustomers();

        [OperationContract]
        int AddCustomer(string FirstName, string LastName);

        [OperationContract]
        void UpdateCustomer(Customer c);

        [OperationContract]
        void DeleteCustomer(int customerId);
    }
}
