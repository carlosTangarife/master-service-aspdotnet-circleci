using System.Runtime.Serialization;

namespace Renting.MasterServices.Core.Dtos
{
    [DataContract(Name = "responseGenericMessage")]
    public class ResponseGenericMessage
    {
        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "messageType")]
        public string MessageType { get; set; }

        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }
    }
}
