using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Exchange a Facebook AccessToken for a MegaStarz REST ticket
        /// </summary>
        /// <param name="request">Ticket request</param>
        /// <param name="callback">Callback function that recieves with ticket & star info</param>
        public void GetTicketAsync(GetTicketRequest request, Action<GetTicketResponse> callback)
        {
            RestRequest restRequest = new RestRequest("GetTicket", Method.POST);

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Accept", "application/xml");

           restRequest.AddBody(request);

            ExecuteAsync<GetTicketResponse>(restRequest, callback);
        }
        
            
        /// <summary>
        /// Upload a recorded file to MegaStarz songs repository
        /// </summary>
        /// <param name="ticket">A valid REST ticket</param>
        /// <param name="songId">The song id this recording belongs to</param>
        /// <param name="fileStream">The recorded stream</param>
        /// <param name="callback">A callback function that recieves file id and play url</param>
        public void UploadRecordingAsync(string ticket, int songId, Stream fileStream, Action<UploadRecordingResponse> callback)
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
        
        /// <summary>
        /// Get all avialable songs to record
        /// </summary>
        /// <param name="callback">A callback function that recieves a list of songs</param>
        public void GetSongsAsync(Action<List<SongResponse>> callback )
        {
            RestRequest request = new RestRequest("Songs", Method.GET);

            ExecuteAsync<List<SongResponse>>(request, callback);
        }
        
    }
}
