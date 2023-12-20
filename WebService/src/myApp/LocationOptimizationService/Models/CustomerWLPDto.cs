using MyService.Algorithms;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace MyService.Algorithmss
{
    [DataContract]
    public class CustomerWLPDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public int Demand { get; set; }
        [DataMember]
        public List<CustomerWarehouseCostWlpDto> CustomerWarehouseCostWlpDtos { get; set; }
    }
    [DataContract]
    public class ListCustomerWarehouseCosts
    {
        [DataMember]
        public List<CustomerWarehouseCostWlpDto> CustomerWarehouseCostWlpDto { get; set; }
        public ListCustomerWarehouseCosts()
        {
            CustomerWarehouseCostWlpDto = new List<CustomerWarehouseCostWlpDto>();   
        }
    }


}
