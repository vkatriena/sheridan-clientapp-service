using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Final_Katrina_Mehta
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.

    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    public class Service1 : IService
    {

        public List<CustomerModel> GetCustomers()
        {
            using (var context = new SalesEntities())
            {
                List<Customer> custList = new List<Customer>();
                custList = context.Customers.ToList<Customer>();
                List<CustomerModel> list = new List<CustomerModel>();
                foreach (Customer item in custList)
                {
                    list.Add(new CustomerModel { CustomerID = item.CustomerID, CustomerName = item.CustomerName,
                        YTDOrders = item.YTDOrders, YTDSales = item.YTDSales });
                }

                return list;

            }
        }



        public void InsertCustomer(string name, int orders, int sales)
        {
            using (var context = new SalesEntities())
            {

                context.Customers.Add(new Customer { CustomerName = name.ToUpper(), YTDOrders = orders, YTDSales = sales });
                context.SaveChanges();

            }
        }

        public void UpdateCustomer(int id, string name, int orders, int sales)
        {
            using (var context = new SalesEntities())
            {
                Customer item = context.Customers.Find(id);
                if (item != null)
                {
                    item.CustomerName = name.ToUpper();
                    item.YTDOrders = orders;
                    item.YTDSales = sales;
                }
                context.SaveChanges();

            }
        }

        public void DeleteCustomer(int id)
        {
            using (var context = new SalesEntities())
            {
                Customer item = context.Customers.Find(id);
                if (item != null)
                    context.Customers.Remove(item);

                context.SaveChanges();
            }

        }

        public List<CustomerModel> GetCustomersByName(string name)
        {
            using (var context = new SalesEntities())
            {

                List<Customer> custList = new List<Customer>();
                custList = context.Customers.Where(x => x.CustomerName.Contains(name)).ToList();
                List<CustomerModel> list = new List<CustomerModel>();
                foreach (Customer item in custList)
                {
                    list.Add(new CustomerModel
                    {
                        CustomerID = item.CustomerID,
                        CustomerName = item.CustomerName,
                        YTDOrders = item.YTDOrders,
                        YTDSales = item.YTDSales
                    });
                }

                return list;
            }

        }


    

        //ORDERS
        public void InsertOrder(int customerId, DateTime orderedDate, DateTime filledDate, string status, int amount)
        {
            using (var context = new SalesEntities())
            {

                context.Orders.Add(new Order 
                    { 
                    CustomerID = customerId, 
                    OrderDate = orderedDate,
                    FilledDate = filledDate, 
                    Status = status.ToUpper(), 
                    Amount = amount
                });

                context.SaveChanges();

            }
        }
        public void UpdateOrder(int id, int customerId, DateTime orderedDate, DateTime filledDate, string status, int amount)
        {
            using (var context = new SalesEntities())
            {
                var item = context.Orders.Find(id);
                if (item != null)
                {
                    item.CustomerID = customerId;
                    item.OrderDate = orderedDate;
                    item.FilledDate = filledDate;
                    item.Status = status.ToUpper();
                    item.Amount = amount;
                }
                context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            using (var context = new SalesEntities())
            {
                var item = context.Orders.Find(id);
                if (item != null)
                    context.Orders.Remove(item);
                context.SaveChanges();
            }
        }


        public List<OrderModel> GetOrders()
        {
            using (var context = new SalesEntities())
            {
                List<Order> orderList = new List<Order>();
                orderList = context.Orders.ToList<Order>();
                List<OrderModel> list = new List<OrderModel>();
                foreach (Order item in orderList)
                {
                    list.Add(new OrderModel
                    {
                        OrderID = item.OrderID,
                        CustomerID = item.CustomerID,
                        OrderDate = item.OrderDate,
                        FilledDate = item.FilledDate,
                        Status = item.Status,
                        Amount = item.Amount

                    }); ;
                }

                return list;

            }
        }
    }
}
