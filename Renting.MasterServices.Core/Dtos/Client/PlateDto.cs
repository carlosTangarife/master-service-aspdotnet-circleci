using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class PlateDto : EntityBase
    {
        [DataMember(Name= "Placa")]
        public string PlateCode { get; set; }

        [DataMember(Name= "Marca")]
        public string Brand { get; set; }

        [DataMember(Name= "Descripcion")]
        public string Description { get; set; }

        [DataMember(Name= "Descripcion larga")]
        public string DescriptionLarge { get; set; }

        [DataMember(Name= "Tipo vehículo")]
        public string VehicleTpe { get; set; }
    }
}
