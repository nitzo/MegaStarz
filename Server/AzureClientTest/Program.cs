using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Megastar.AzureWP7Client;
using Megastar.RestServices.Library.Entities;

namespace AzureClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WP7Client();

            var req = new GetTicketRequest();
            req.AccessToken =
                "148836425163954|2.AQDPOo6a1szM-Vbe.3600.1312549200.0-788734623|L9jzdfw5GD-drMMlFOXJpjbqXgw";

            client.GetTicketAsync(req,
                (response) =>
                    {
                        var a = response.Star;
                    });


            System.Threading.Thread.CurrentThread.Suspend();
        }
    }
}
