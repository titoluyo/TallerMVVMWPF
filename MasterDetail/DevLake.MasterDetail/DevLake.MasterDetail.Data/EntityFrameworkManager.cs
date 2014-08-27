using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevLake.MasterDetail.Data
{
    class EntityFrameworkManager : IDataManager
    {
        ICustomerManager IDataManager.GetCustomerManager()
        {
            return new CustomerManager();
        }

        IOrderManager IDataManager.GetOrderManager()
        {
            return new OrderManager();
        }

        class CustomerManager : ICustomerManager
        {     
            int ICustomerManager.Add(string firstName, string lastName)
            {
                using (var context = new MasterDetailEntities())
                {
                    Customer c = new Customer { FirstName = firstName, LastName = lastName };
                    context.Customers.AddObject(c);
                    context.SaveChanges();
                    return c.CustomerId;
                }
            }

            void ICustomerManager.Delete(int customerId)
            {
                using (var context = new MasterDetailEntities())
                {
                    Customer c = context.Customers.Where(i => i.CustomerId == customerId).First();
                    context.DeleteObject(c);
                    context.SaveChanges();
                }
            }

            void ICustomerManager.Update(int customerId, string firstName, string lastName)
            {
                using (var context = new MasterDetailEntities())
                {
                    Customer c = context.Customers.Where(i => i.CustomerId == customerId).First();
                    c.FirstName = firstName;
                    c.LastName = lastName;
                    context.SaveChanges();
                }
            }

            List<Customer> ICustomerManager.FindAll()
            {
                using (var context = new MasterDetailEntities())
                {
                    return context.Customers.ToList();
                }
            }

            Customer ICustomerManager.FindByOrder(int orderId)
            {
                using (var context = new MasterDetailEntities())
                {
                    return context.Orders.Where(i => i.OrderId == orderId).First().Customer;
                }
            }
        }

        class OrderManager : IOrderManager
        {
            int IOrderManager.Add(string description, int quantity, int customerId)
            {
                using (var context = new MasterDetailEntities())
                {
                    Order order = new Order { Description = description, Quantity = quantity };
                    context.Customers.First(i => i.CustomerId == customerId).Orders.Add(order);
                    context.SaveChanges();
                    return order.OrderId;
                }
            }

            void IOrderManager.Delete(int orderId)
            {
                using (var context = new MasterDetailEntities())
                {
                    Order a = context.Orders.Where(i => i.OrderId == orderId).First();
                    context.DeleteObject(a);
                    context.SaveChanges();
                }
            }

            void IOrderManager.Update(int orderId, string description, int quantity)
            {
                using (var context = new MasterDetailEntities())
                {
                    Order a = context.Orders.Where(i => i.OrderId == orderId).First();
                    a.Description = description;
                    a.Quantity = quantity;
                    context.SaveChanges();
                }
            }

            List<Order> IOrderManager.FindByCustomer(int customerId)
            {
                using (var context = new MasterDetailEntities())
                {
                    return context.Customers.First(i => i.CustomerId == customerId).Orders.ToList();
                }
            }

            Order IOrderManager.Find(int orderId)
            {
                using (var context = new MasterDetailEntities())
                {
                    return context.Orders.First(i => i.OrderId == orderId);
                }
            }
        }

    }
}
