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

        public SongStarLink CreateSongStarLink()
        {
            var songStarLink = _context.SongStarLinks.CreateObject();

            _context.SongStarLinks.AddObject(songStarLink);

            return songStarLink;
        }

        
        public Star GetStar(string faceBookId)
        {
            return _context.Stars.Where(s => s.FacebookId == faceBookId).FirstOrDefault();
        }

        public Song GetSong(int id)
        {
            return _context.Songs.Where(s => s.Id == id).FirstOrDefault();
        }


        public List<Song>  GetSongs()
        {
            return _context.Songs.Include("Artist").ToList();
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
