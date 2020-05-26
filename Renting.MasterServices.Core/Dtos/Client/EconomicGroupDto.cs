using System;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Client
{
    public class EconomicGroupDto : EntityBase
    {
        [DataMember(Name= "Id")]
        public int Id { get; set; }

        [DataMember(Name= "Grupo económico")]
        public string EconomicGroupName { get; set; }

        public bool Selected { get; set; }
    }
}
