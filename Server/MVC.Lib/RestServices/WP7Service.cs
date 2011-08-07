using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using AutoMapper;
using Facebook;
using MegaStar.Data.Library;
using MegaStar.Data.Library.Entities;
using Megastar.RestServices.Library.Entities;

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
        #region REST Operations
        
        /// <summary>
        /// Returns a ticket associated with an access Token.
        /// </summary>
        /// <param name="request">Facebook Access token of the User</param>
        /// <returns>A valid ticket and the User's detials from facebook</returns>
        [WebInvoke(Method = "POST", UriTemplate = "GetTicket")]
        [Description("Get a ticket to enable usage of this service")]
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
           

            StarResponse star = UpdateStarInDB(user);


            var response = new GetTicketResponse()
                               {
                                   Star = star,
                                   Ticket = TicketHandler.GenerateNewTicket(request.AccessToken, star.FacebookId) 
                               };

            return response;
        }

        /// <summary>
        /// Upload a new recorded song to the blob storage.
        /// Request's content type must be text/plain!
        /// </summary>
        /// <param name="ticket">The ticket string received from GetTicket()</param>
        /// <param name="songId">The song id to which this recording belongs to</param>
        /// <param name="file">A byte content file</param>
        /// <returns>Returns the ID of the file in the blob and it's URL</returns>
        [WebInvoke(Method = "POST", UriTemplate = "UploadSong/{ticket}/{songId}")]
        [Description("Upload a recorded song to the application. When using set content type to text/plain!")]
        public UploadRecordingResponse UploadSong(string ticket, string songId, Stream file)
        {
            var t = TicketHandler.GetVerifiedTicket(ticket);

            int sid;

            if (!Int32.TryParse(songId, out sid))
                return null; //TODO: Return Bad Request

            if (t == null)
                return null; //TODO: Return Bad Request

            var storage = new BlobStorageManager("recordedsongs"); //TODO:Get from web.config

            string fileGuid;

            try
            {
                fileGuid = storage.UploadStream(file, "video/mp4");
            }
            catch (Exception e)
            {
                return null; //TODO: Return Bas request
            }


            using (var _repo = new MegaStarzRepository())
            {
                var star = _repo.GetStar(t.starId);

                var song = _repo.GetSong(sid);

                if (song == null || star == null)
                {
                    storage.DeleteBlob(fileGuid);
                    return null; //TODO: Return Bad Request
                }

                var songStarLink = _repo.CreateSongStarLink();

                songStarLink.Star = star;
                songStarLink.Song = song;
                songStarLink.FileGuid = fileGuid;

                try
                {
                    _repo.Save();
                }
                catch (Exception e)
                {
                    storage.DeleteBlob(fileGuid);
                    return null; //TODO: Return Bad Request
                }
            }

            return new UploadRecordingResponse(){id = fileGuid, playUrl = storage.GetBlobUri(fileGuid).ToString()};
        }

        /// <summary>
        /// Get all songs on server avialable for playback
        /// </summary>
        /// <returns>List of songs avialable</returns>
        [WebGet(UriTemplate = "Songs")]
        [Description("Get all songs avialable")]
        public List<SongResponse> GetSongs()
        {
            List<SongResponse> result = new List<SongResponse>();

            using (var _repo = new MegaStarzRepository())
            {
                var songList = _repo.GetSongs();

                foreach (var song in songList)
                {
                    var s = new SongResponse()
                                {
                                    id = song.Id,
                                    artistName = song.Artist.Name,
                                    name = song.Name,
                                    length = song.Length,
                                    playUrl = song.PlayUrl
                                };

                    result.Add(s);
                }
                
            }

            return result;
        }

        #endregion

        #region Private Methods

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
       #endregion
 
    }
}
