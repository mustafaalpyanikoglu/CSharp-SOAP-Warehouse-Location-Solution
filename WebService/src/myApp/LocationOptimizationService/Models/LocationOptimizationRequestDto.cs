using MyService.Algorithmss;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Application.Features.LocationSolvers.Dtos
{
    [DataContract]
    public class LocationOptimizationRequestDto
    {
        [DataMember]
        public List<CustomerWLPDto> CustomerWLPDtos { get; set; }
        [DataMember]
        public List<WarehouseWLPDto> WarehouseWLPDtos { get; set; }
    }
}