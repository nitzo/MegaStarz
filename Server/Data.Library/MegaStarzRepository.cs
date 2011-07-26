using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaStar.Data.Library.Entities;

namespace MegaStar.Data.Library
{
    public class MegaStarzRepository
    {

        private MegaStarzEntities _context;


        public MegaStarzRepository()
        {
            _context = new MegaStarzEntities();   
        }


        public Star CreateStarEntry ()
        {
            return _context.Stars.CreateObject();
        }


        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
