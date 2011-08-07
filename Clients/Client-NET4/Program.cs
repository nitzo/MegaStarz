using System;
using System.IO;
using Megastar.Client.Library;
using Megastar.RestServices.Library.Entities;

namespace AzureClientTester
{
    class Program
    {
        static void Main(string[] args)
        {
           // var client = new WP7Client("http://localhost:88/Services/WP7");
            var client = new MegaStarzClient();

            var req = new GetTicketRequest();
            req.AccessToken =
                "148836425163954|2.AQC6yDgbSMJCasPO.3600.1312725600.0-788734623|IqbfIqIVHI5JF3byke-I6jDjm7Y";

            client.GetTicketAsync(req,
                (response) =>
                    {
                        FileStream fs = null;

                        try
                        {
                            fs = new FileStream(@"C:\Users\nitzo\Downloads\Bulletproof_Monk_Sample.mp4", FileMode.Open, FileAccess.Read);
                        }
                        catch (Exception e)
                        {
                            
                        }


                        client.UploadRecordingAsync(response.Ticket.ticket, "1", fs, (recResponse) =>
                                                                              {
                                                                                  Console.WriteLine(recResponse.playUrl);
                                                                              });




                });


            System.Threading.Thread.CurrentThread.Suspend();
        }
    }
}
