using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class ClientUserDto : EntityBase
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }

        [DataMember(Name = "Nombre cliente")]
        public string ClientName { get; set; }

        [DataMember(Name = "Id grupo económico")]
        public int EconomicGroupId { get; set; }

        public bool Selected { get; set; }
    }
}
