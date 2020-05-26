using System.ComponentModel.DataAnnotations.Schema;

namespace Renting.MasterServices.Domain.Entities.Client
{
    public class State : EntityBase
    {
        [Column(name: "lngIdEstado")]
        public int Id { get; set; }

        [Column(name: "intTipoEstado")]
        public short? StateType { get; set; }

        [Column(name: "strEstado")]
        public string StateName { get; set; }

        [Column(name: "intOrden")]
        public float? Order { get; set; }

        [Column(name: "strColor")]
        public string Color { get; set; }

        [Column(name: "lngGroupBy")]
        public short? GroupBy { get; set; }
    }
}
