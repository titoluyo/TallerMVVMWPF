using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace DevLake.MasterDetail.Service
{
    [DataContract]
    public class Customer
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        internal Customer(Business.Customer i)
        {
            CustomerId = i.CustomerId;
            FirstName = i.FirstName;
            LastName = i.LastName;
        }       

    }
}