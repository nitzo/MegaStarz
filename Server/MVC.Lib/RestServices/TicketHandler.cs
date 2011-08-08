using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaStar.Data.Library.Entities;
using Megastar.RestServices.Library.Entities;

namespace MegaStar.MVC.Lib.RestServices
{
    public static class TicketHandler
    {
        private static readonly Dictionary<string, Ticket> ticketDictionary = new Dictionary<string, Ticket>();
       
       public static Ticket GenerateNewTicket(string accessToken, string facebookId)
       {
           var t = new Ticket();

           //Save Access token & starId to Ticket
           t.accessToken = accessToken;
           t.starId = facebookId;

           //Generate a new ticket & set expiry
           t.ticket = Guid.NewGuid().ToString();
           t.expires = DateTime.UtcNow.AddHours(2); //TODO: Set exipiery settings in web.config

           lock (ticketDictionary)
           {
               ticketDictionary.Add(t.ticket, t);
           }

           return t;
       }

        public static Ticket GetVerifiedTicket(string ticket)
        {
            Ticket t;

            //Try getting ticket
            if (!ticketDictionary.TryGetValue(ticket, out t))
                return null;

            //Check ticket expiery time
            if (DateTime.UtcNow > t.expires)
            {
                lock (ticketDictionary)
                {
                    ticketDictionary.Remove(ticket);
                }
                return null;
            }

            return t;
        }

    }
}
