using System;
using System.Runtime.Serialization;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class Ticket
    {
        [DataMember]
        public string ticket;

        [DataMember]
        public DateTime expires;

        public string accessToken;  //TODO: Hide this in response to GetTicket (Make Internal)?

        public string starId;       //TODO: Hide this in response to GetTicket (Make Internal)?
    }
}
