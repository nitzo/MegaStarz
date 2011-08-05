using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace MegaStar.AzureClient
{
    public class Client
    {
        private Uri _baseUrl;
        
        public Client (string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
        }

        public Client()
        {
            _baseUrl = new Uri("http://megastarz.cloudapp.net/Services/");
        }

  
    }
}
