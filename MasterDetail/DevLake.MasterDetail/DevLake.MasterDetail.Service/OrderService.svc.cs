using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Business = DevLake.MasterDetail.Business;

namespace DevLake.MasterDetail.Service
{
    public class OrderService : IOrderService
    {
        List<Order> IOrderService.GetOrderByCustomer(int customerId)
        {
            List<Order> result = new List<Order>();
            foreach (Business.Order i in Business.CustomerManager.Instance().GetCustomer(customerId).Orders)
                result.Add(new Order(i));
            return result;
        }

        int IOrderService.AddOrder(int customerId, Order i)
        {
            return Business.CustomerManager.Instance().GetCustomer(customerId).AddOrder(i.Description, i.Quantity);
        }

        void IOrderService.UpdateOrder(Order i)
        {
            new Business.Order(i.OrderId).Customer.UpdateOrder(new Business.Order(i.OrderId, i.Description, i.Quantity));
        }

        void IOrderService.DeleteOrder(int orderId)
        {
            new Business.Order(orderId).Customer.DeleteOrder(orderId);
        }

        Order IOrderService.GetOrder(int orderId)
        {
            Business.Order i = new Business.Order(orderId);
            i.Refresh();
            return new Order(i);
        }

    }
}
