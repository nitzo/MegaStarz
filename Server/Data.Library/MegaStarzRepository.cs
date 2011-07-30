using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MegaStar.Data.Library.Entities;

namespace MegaStar.Data.Library
{
    public class MegaStarzRepository : IDisposable
    {

        private MegaStarzEntities _context;


        public MegaStarzRepository()
        {
            _context = new MegaStarzEntities();   
        }


        public Star CreateStarEntry()
        {
            var star = _context.Stars.CreateObject();

            _context.Stars.AddObject(star);

            return star;
        }

        public Star GetStar(string faceBookId)
        {
            return _context.Stars.Where(s => s.FacebookId == faceBookId).FirstOrDefault();
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
