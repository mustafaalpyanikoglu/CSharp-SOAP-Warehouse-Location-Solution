
using System.Runtime.Serialization;

namespace Application.Features.LocationSolvers.Dtos
{
    [DataContract]
    public class WarehouseWLPDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Capacity { get; set; }
        [DataMember]
        public double SetupCost { get; set; }
    }

}

