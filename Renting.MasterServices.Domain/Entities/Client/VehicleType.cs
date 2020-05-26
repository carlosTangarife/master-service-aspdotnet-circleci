using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class VehicleType : EntityBase
    {
        [Column(name: "IdTipoVehiculo")]
        public int Id { get; set; }

        [Column(name: "strTipoVehiculo")]
        public string VehicleTypeName { get; set; }
    }
}
