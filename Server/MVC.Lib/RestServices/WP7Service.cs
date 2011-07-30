using System;
using System.Dynamic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using AutoMapper;
using Facebook;
using MegaStar.Data.Library;
using MegaStar.Data.Library.Entities;

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
        

        [WebInvoke(Method = "POST", UriTemplate = "GetTicket")]
        public GetTicketResponse GetTicket(GetTicketRequest request)
        {

            FacebookClient fb = new FacebookClient(request.AccessToken);
            
           dynamic user;
        
           try
           {
               user = fb.Get("/me");
           }
           catch (FacebookOAuthException e)
           {
               //ToDO: Return Bad Request
               return null;
           }
           catch (Exception e)
           {
               //ToDO: Return Bad Request
               return null;
           }

           if (user.id == null)
               return null; //TODO: Return bas request
           

            var star = UpdateStarInDB(user);


            var response = new GetTicketResponse()
                               {
                                   Star = star,

                                    //TODO: Implement ticketing system
                                   Ticket = new Ticket() {ticket = "123456", expires = DateTime.Now.AddHours(2)}
                               };

            return response;
        }


        private StarResponse UpdateStarInDB(dynamic user)
        {
            StarResponse starResponse = new StarResponse();

            using (var repo = new MegaStarzRepository())
            {
                var star = repo.GetStar(user.id);

                //User has been found in repo
                if (star != null)
                {


                    //User's facebook profile has been updated - copy changes to DB
                    if (user.updated_time != null && star.UpdateDate < DateTime.Parse(user.updated_time))
                        StarFromFBUser(star, user);
                }
                else //Star not found in DB, create a new Star
                {
                    star = repo.CreateStarEntry();
                    star.FacebookId = user.id; 
                    StarFromFBUser(star, user);
                }
                repo.Save();
                
                //Map star object to light-wieght starResponse
                Mapper.DynamicMap(star, starResponse);
            }

            return starResponse;
        }

        private void StarFromFBUser(Star star, dynamic user)
        {
           
            star.UpdateDate = DateTime.Parse(user.updated_time);
            star.FirstName = user.first_name;
            star.LastName = user.last_name;
            star.Gender = user.gender;
            star.Locale = user.locale;
            star.Email = user.email;

            DateTime bday;

            if (DateTime.TryParse(user.birthday, out bday))
                star.Birthday = bday;

            //TODO: Get profile picture

            //star.picture = user.picture;
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
