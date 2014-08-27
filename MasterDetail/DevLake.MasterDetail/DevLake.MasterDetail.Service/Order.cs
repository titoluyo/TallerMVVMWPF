using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DevLake.MasterDetail.Service
{
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        internal Order(Business.Order i)
        {
            OrderId = i.OrderId;
            Description = i.Description;
            Quantity = i.Quantity;
        }
        
    }
}