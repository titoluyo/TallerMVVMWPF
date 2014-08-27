using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DevLake.MasterDetail.Service
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        List<Order> GetOrderByCustomer(int customerId);

        [OperationContract]
        int AddOrder(int customerId, Order i);

        [OperationContract]
        void UpdateOrder(Order i);

        [OperationContract]
        void DeleteOrder(int orderId);

        [OperationContract]
        Order GetOrder(int orderId);
    }
}
