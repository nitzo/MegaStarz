using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Facebook.Web;
using Facebook.Web.Mvc;
using MegaStar.Data.Library;
using MegaStar.MVC.Lib.Models;
using MegaStar.MVC.Lib.RestServices;
using Megastar.RestServices.Library.Entities;

namespace Megastar.MVC.Web.Controllers
{
    public class FanPageController : Controller
    {
        //
        // GET: /FanPage/
        //[CanvasAuthorize(Permissions = "user_about_me,user_birthday,email")]
        public ActionResult Index(string id)
        {

            using (var _repo = new MegaStarzRepository())
            {
                var star = _repo.GetStar(id);

                if (star == null)
                    return View("Error");

                var newestSong = star.SongStarLinks.OrderBy(s => s.InsertDate).FirstOrDefault();

                var model = new StarVM();

                Mapper.DynamicMap(star, model.Star);
                if (newestSong != null)
                {
                    var storage = new BlobStorageManager("recordedsongs"); //TODO: Get from web.config


                    model.NewestSong = new UploadRecordingResponse()
                                           {
                                               id = newestSong.FileGuid,
                                               playUrl = storage.GetBlobUri(newestSong.FileGuid).ToString()
                                           };
                }

                return View(model);
            }

            

            
        }

    }
}
