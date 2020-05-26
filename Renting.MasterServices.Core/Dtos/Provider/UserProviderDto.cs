using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Provider
{
    public class UserProviderDto : EntityBase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "providerId")]
        public int ProviderId { get; set; }

        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; }

        [DataMember(Name = "creationDate")]
        public DateTime CreationDate { get; set; }

        [DataMember(Name = "modifiedBy")]
        public string ModifiedBy { get; set; }

        [DataMember(Name = "lastModification")]
        public DateTime? LastModification { get; set; }

        [DataMember(Name = "emailUser")]
        public string EmailUser { get; set; }
    }
}
