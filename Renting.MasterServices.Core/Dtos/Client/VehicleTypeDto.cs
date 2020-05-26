using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class VehicleTypeDto : EntityBase
    {
        [DataMember(Name= "Id")]
        public int Id { get; set; }

        [DataMember(Name= "Tipo vehículo")]
        public string VehicleTypeName { get; set; }
    }
}
