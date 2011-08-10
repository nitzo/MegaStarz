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
            //var client = new MegaStarzClient("http://localhost:81/Services/WP7");
            var client = new MegaStarzClient();

            var req = new GetTicketRequest();
            req.AccessToken =
                "150708541673346|2.AQCt99tG8raLrjhw.3600.1313024400.0-788734623|ELskl_xh1EwdN3y_e61rjmYjjQk";

            client.GetTicketAsync(req,
                (response) =>
                    {
                        FileStream fs = null;

                        try
                        {
                            fs = new FileStream(@"C:\1020.log", FileMode.Open, FileAccess.Read);
                        }
                        catch (Exception e)
                        {
                            
                        }


                        client.UploadRecordingAsync(response.Ticket.ticket, 1, fs, (recResponse) =>
                                                                              {
                                                                                  Console.WriteLine(recResponse.playUrl);
                                                                              });




                });

//            client.GetSongsAsync((response) =>
//                                     {
//                                         foreach (var songResponse in response)
//                                         {
//                                             Console.WriteLine(string.Format("Song ({0}): \"{1}\", ({2}). Length {3}, PlayUrl: {4}", songResponse.id, songResponse.name, songResponse.artistName, songResponse.length, songResponse.playUrl));
//                                         }
//                                     });

            System.Threading.Thread.CurrentThread.Suspend();
        }
    }
}
