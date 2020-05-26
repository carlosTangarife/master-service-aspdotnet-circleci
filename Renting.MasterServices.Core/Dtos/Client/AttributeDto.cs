using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class AttributeDto : EntityBase
    {
        [DataMember(Name = "Id atributo")]
        public int Id { get; set; }

        [DataMember(Name = "Nombre atributo")]
        public string AttributeName { get; set; }

        [DataMember(Name = "Orden")]
        public short Order { get; set; }
    }
}
