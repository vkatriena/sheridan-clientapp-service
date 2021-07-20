using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Final_Katrina_Mehta
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {



        //Customer
        [OperationContract]
        void InsertCustomer(string name, int orders, int sales);

        [OperationContract]
        void UpdateCustomer(int id, string name, int orders, int sales);

        [OperationContract]
        void DeleteCustomer(int id);

        [OperationContract]
        List<CustomerModel> GetCustomers();

        //Orders


        [OperationContract]
        List<OrderModel> GetOrders();

        [OperationContract]
        void InsertOrder(int customerId, DateTime orderedDate, DateTime filledDate, string status, int amount);
        [OperationContract]
        void UpdateOrder(int id, int customerId, DateTime orderedDate, DateTime filledDate, string status, int amount);

        [OperationContract]
        void DeleteOrder(int id);

       


    }

}
