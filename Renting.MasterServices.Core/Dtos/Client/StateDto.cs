using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class StateDto : EntityBase
    {
        [DataMember(Name = "Id estado")]
        public int Id { get; set; }

        [DataMember(Name = "Tipo estado")]
        public short? StateType { get; set; }

        [DataMember(Name = "Nombre estado")]
        public string StateName { get; set; }

        [DataMember(Name = "Orden")]
        public float? Order { get; set; }

        [DataMember(Name = "Color")]
        public string Color { get; set; }

        [DataMember(Name = "Agrupar Por")]
        public short? GroupBy { get; set; }
    }
}
