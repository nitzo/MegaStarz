using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MegaStar.MVC.Lib
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
