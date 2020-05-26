using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class Plate : EntityBase
    {
        [Column(name: "StrPlaca")]
        public string PlateCode { get; set; }

        [Column(name: "strMarca")]
        public string Brand { get; set; }

        [Column(name: "strDescripcion")]
        public string Description { get; set; }

        [Column(name: "strFullDescripcion")]
        public string DescriptionLarge { get; set; }

        [Column(name: "strTipoVehiculo")]
        public string VehicleTpe { get; set; }
    }
}
