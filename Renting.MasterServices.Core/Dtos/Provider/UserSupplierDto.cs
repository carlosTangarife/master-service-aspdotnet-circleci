using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Provider
{
    public class UserSupplierDto: EntityBase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "supplierId")]
        public int SupplierId { get; set; }

        [DataMember(Name = "emailUser")]
        public string EmailUser { get; set; }
    }
}
