using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class ParameterDto : EntityBase
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Nombre")]
        public string Name { get; set; }

        [DataMember(Name = "Descripcion")]
        public string Description { get; set; }

        [DataMember(Name = "Tipo")]
        public string Type { get; set; }

        [DataMember(Name = "Valor")]
        public string Value { get; set; }

        [DataMember(Name = "UsuarioIngreso")]
        public string UserLoginName { get; set; }

        [DataMember(Name = "FechaIngreso")]
        public DateTime LoginDate { get; set; }
    }
}
