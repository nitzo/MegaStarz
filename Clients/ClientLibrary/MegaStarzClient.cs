using System;
using System.IO;
using System.Net;
using Megastar.RestServices.Library.Entities;
using RestSharp;


namespace Megastar.Client.Library
{
    public class MegaStarzClient
    {
        private string _baseUrl;
        
        public MegaStarzClient (string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public MegaStarzClient()
        {
            

            _baseUrl = "http://megastarz.cloudapp.net/Services/WP7";
        }

        private void ExecuteAsync<T>(RestRequest restRequest, Action<T> callback ) where T : new ()
        {
            RestClient client = new RestClient(_baseUrl);

            //Register our custom serializer
            client.AddHandler("application/xml", new MegaStarXmlSerializer());
            
            //Perform request and Pass Response of type T to user's callback
            client.ExecuteAsync<T>(restRequest, (response) =>
                                                    {

                                                       if (response.StatusCode != HttpStatusCode.OK)
                                                        {
                                                            throw new Exception("Could not perform request. ", response.ErrorException);
                                                        }
                                                            
                                                        callback(response.Data);
                                                    });
        }


        public void GetTicketAsync(GetTicketRequest request, Action<GetTicketResponse> callback)
        {
            RestRequest restRequest = new RestRequest("GetTicket", Method.POST);

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Accept", "application/xml");

           restRequest.AddBody(request);

            ExecuteAsync<GetTicketResponse>(restRequest, callback);
        }
        
            

        public void UploadRecordingAsync(string ticket, string songId, Stream fileStream, Action<UploadRecordingResponse> callback)
        {

            string resource = String.Format("UploadSong/{0}/{1}", ticket, songId);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(_baseUrl + "/" + resource);
            req.Method = "POST";
            req.ContentType = "text/plain"; //Content Type MUST BE text/plain!

#if !WINDOWS_PHONE
            req.AllowWriteStreamBuffering = false;
            req.SendChunked = true;
            req.Timeout = 4800000;
#endif

            req.BeginGetRequestStream((asynchronousRequestResult) =>
            {
                HttpWebRequest r = (HttpWebRequest)asynchronousRequestResult.AsyncState;

                using (Stream requestStream = r.EndGetRequestStream(asynchronousRequestResult))
                {
                    fileStream.CopyTo(requestStream);
                }                               

                r.BeginGetResponse((asynchronousResponseResult) =>
                {
                    HttpWebRequest request = (HttpWebRequest)asynchronousResponseResult.AsyncState;

                    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResponseResult);

                    MegaStarXmlSerializer serializer = new MegaStarXmlSerializer();

                    var result = serializer.Deserialize<UploadRecordingResponse>(response.GetResponseStream());

                    callback(result);

                }, r);


            }, req);


            

           

          
        }
        
    }
}
