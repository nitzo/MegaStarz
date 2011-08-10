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
            var client = new MegaStarzClient("http://localhost:81/Services/WP7");
            //var client = new MegaStarzClient();

            var req = new GetTicketRequest();
            req.AccessToken =
                "148836425163954|2.AQAQ-BVk8M_rPYWc.3600.1313010000.0-788734623|RISVoJOaVRVlsed6DjGBm0ijHV8";

            client.GetTicketAsync(req,
                (response) =>
                    {
                        FileStream fs = null;

                        try
                        {
                            fs = new FileStream(@"C:\projects\MegastrazWP7\Videos\Abba_Mamma_Mia.wmv", FileMode.Open, FileAccess.Read);
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
