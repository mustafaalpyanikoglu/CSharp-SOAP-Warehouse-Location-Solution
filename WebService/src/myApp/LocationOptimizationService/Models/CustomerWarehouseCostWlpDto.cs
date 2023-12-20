using System.Runtime.Serialization;

namespace MyService.Algorithms
{
    [DataContract]
    public class CustomerWarehouseCostWlpDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int WarehouseID { get; set; }
        [DataMember]
        public double Cost { get; set; }
    }
}
