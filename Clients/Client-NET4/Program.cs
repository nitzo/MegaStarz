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
                "148836425163954|2.AQBGsPpmveZhgIK0.3600.1312671600.0-788734623|hUDBu-6iOb8dTM1GbvPKT7wdRME";

            client.GetTicketAsync(req,
                (response) =>
                    {
                        FileStream fs = null;

                        try
                        {
                            fs = new FileStream("C:\\1020.log", FileMode.Open, FileAccess.Read);
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
