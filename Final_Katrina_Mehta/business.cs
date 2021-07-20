using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Final_Katrina_Mehta
{
    public class business
    {
    }
    [DataContract]
    public class CustomerModel
    {
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string CustomerName { get; set; }
        [DataMember]
        public int YTDOrders { get; set; }
        [DataMember]
        public int YTDSales { get; set; }


    }

    [DataContract]
    public class OrderModel
    {
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public System.DateTime OrderDate { get; set; }
        [DataMember]
        public Nullable<System.DateTime> FilledDate { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int Amount { get; set; }
    }
}