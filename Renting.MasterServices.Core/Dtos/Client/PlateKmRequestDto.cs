using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class PlateKmRequestDto : EntityBase
    {
        [DataMember(Name= "Placa")]
        public string PlateCode { get; set; }

        [DataMember(Name= "Ultimo contador")]
        public float LastCounter { get; set; }

        [DataMember(Name= "Recorrido century")]
        public decimal RouteCentury { get; set; }

        [DataMember(Name= "Tipo contador")]
        public int CounterType { get; set; }

        [DataMember(Name= "Fecha contador")]
        public DateTime CounterDate { get; set; }
    }
}
