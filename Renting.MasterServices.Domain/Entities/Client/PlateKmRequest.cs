using System;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class PlateKmRequest
    {
        public string PlateCode { get; set; }

        public float LastCounter { get; set; }

        public decimal RouteCentury { get; set; }

        public int CounterType { get; set; }

        public DateTime CounterDate { get; set; }
    }
}
