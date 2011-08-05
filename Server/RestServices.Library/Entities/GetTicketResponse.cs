using System.Runtime.Serialization;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class GetTicketResponse
    {

        [DataMember(IsRequired = true)]
        public Ticket Ticket;

        [DataMember(IsRequired = true)]
        public StarResponse Star;

    }
}
