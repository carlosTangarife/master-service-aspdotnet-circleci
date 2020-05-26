using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos.Provider
{
    public class ProviderDto : EntityBase
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "providerName")]
        public string ProviderName { get; set; }

        [DataMember(Name = "nitProvider")]
        public string NitProvider { get; set; }

        public string ProviderDescription
        {
            get { return $"{NitProvider} - {ProviderName}"; }
        }
        public bool Selected { get; set; }
    }
}