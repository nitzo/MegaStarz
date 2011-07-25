﻿using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace MegaStar.MVC.Lib.RestServices
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class WP7Service
    {
        

        [WebGet(UriTemplate = "GetTicket/{accessToken}")]
        public Ticket GetTicket(string accessToken)
        {
            
           

            return new Ticket() {ticket = "123456", expires = DateTime.Now.AddHours(2)};
        }

        //[WebInvoke(UriTemplate = "", Method = "POST")]
        //public SampleItem Create(SampleItem instance)
        //{
        //    // TODO: Add the new instance of SampleItem to the collection
        //    throw new NotImplementedException();
        //}

        //[WebGet(UriTemplate = "{id}")]
        //public SampleItem Get(string id)
        //{
        //    // TODO: Return the instance of SampleItem with the given id
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        //public SampleItem Update(string id, SampleItem instance)
        //{
        //    // TODO: Update the given instance of SampleItem in the collection
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        //public void Delete(string id)
        //{
        //    // TODO: Remove the instance of SampleItem with the given id from the collection
        //    throw new NotImplementedException();
        //}

    }
}
