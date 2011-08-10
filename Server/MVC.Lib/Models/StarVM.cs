using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Megastar.RestServices.Library.Entities;

namespace MegaStar.MVC.Lib.Models
{
    public class StarVM
    {

        public StarVM()
        {
            Star = new StarResponse();
        }

        public StarResponse Star;
        public UploadRecordingResponse NewestSong;
    }
}
