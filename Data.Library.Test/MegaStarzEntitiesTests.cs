using System;
using System.Data;
using System.Data.Objects;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MegaStar.Data.Library;
using MegaStar.Data.Library.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Library.Test
{
    [TestClass]
    public class MegaStarzEntitiesTests
    {

        private MegaStarzEntities _context = new MegaStarzEntities();

        [TestMethod]
        public void VerifyStarObject()
        {
            var star = _context.Stars.CreateObject();
            _context.Stars.AddObject(star);

            Assert.IsNotNull(star.InsertDate);
            Assert.IsTrue(IsProxy(star));
            
            star.FirstName = "David";
            star.LastName = "Cohen";
            star.UpdateDate = DateTime.Now;
            star.FacebookId = 1234;
            
            _context.SaveChanges();

            star.FirstName = "David123";

            Assert.AreEqual(_context.ObjectStateManager.GetObjectStateEntry(star).State, EntityState.Modified);
        }

        [TestMethod]
        public void VerifyArtistObject()
        {
            var artist = _context.Artists.CreateObject();
            _context.Artists.AddObject(artist);
            
            Assert.IsNotNull(artist.InsertDate);
            Assert.IsTrue(IsProxy(artist));

            artist.Name = "ABBA";
            
      
            _context.SaveChanges();

            artist.Name = "Queen";

            Assert.AreEqual(_context.ObjectStateManager.GetObjectStateEntry(artist).State, EntityState.Modified);
        }

        [TestMethod]
        public void VerifySongObject()
        {
            var song = _context.Songs.CreateObject();
            _context.Songs.AddObject(song);

            Assert.IsNotNull(song.InsertDate);
            Assert.IsTrue(IsProxy(song));

            song.Name = "Dancing Queen";

            song.Artist = _context.Artists.First();

            _context.SaveChanges();

            song.Name = "We are the champions";

            Assert.AreEqual(_context.ObjectStateManager.GetObjectStateEntry(song).State, EntityState.Modified);
        }

        [TestMethod]
        public void VerifySongStarLink()
        {
            var songStarLink = _context.CreateObject<SongStarLink>();
            _context.SongStarLinks.AddObject(songStarLink);

            songStarLink.Star = _context.Stars.First();
            songStarLink.Song = _context.Songs.First();
            
            Assert.IsNotNull(songStarLink.InsertDate);
            Assert.IsTrue(IsProxy(songStarLink));

            _context.SaveChanges();

            songStarLink.Song = _context.Songs.ToList().Skip(1).First();


            Assert.AreEqual(_context.ObjectStateManager.GetObjectStateEntry(songStarLink).State, EntityState.Modified);
        }

        private bool IsProxy(object obj)
        {
            return obj != null && ObjectContext.GetObjectType(obj.GetType()) != obj.GetType();
        }
    }
}
