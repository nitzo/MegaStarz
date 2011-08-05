using System.Runtime.Serialization;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class GetTicketRequest
    {
        [DataMember]
        public string AccessToken;
    }
}
