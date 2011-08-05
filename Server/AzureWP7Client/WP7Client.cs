using System;
using System.Net;
using Megastar.RestServices.Library.Entities;
using RestSharp;
using RestSharp.Deserializers;

namespace Megastar.AzureWP7Client
{
    public class WP7Client
    {
        private string _baseUrl;
        
        public WP7Client (string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public WP7Client()
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

           restRequest.AddBody(request);

            ExecuteAsync<GetTicketResponse>(restRequest, callback);
        }
        
    }
}
