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
            
            Assert.IsTrue(IsProxy(star));
            
            star.FirstName = "Nitzan";
            star.LastName = "Bar";
            star.UpdateDate = DateTime.Now;
            star.FacebookId = 1234;
            
            _context.SaveChanges();

            star.FirstName = "Nitzan123";

            Assert.AreEqual(_context.ObjectStateManager.GetObjectStateEntry(star).State, EntityState.Modified);
        }


        private bool IsProxy(object obj)
        {
            return obj != null && ObjectContext.GetObjectType(obj.GetType()) != obj.GetType();
        }
    }
}
