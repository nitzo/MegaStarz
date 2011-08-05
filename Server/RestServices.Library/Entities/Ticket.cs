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
    }
}
