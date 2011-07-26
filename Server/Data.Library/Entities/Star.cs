using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaStar.Data.Library.Entities
{
    public partial class Star
    {

        protected Star()
        {
            Id = 0;
            InsertDate = DateTime.Now;
        } 

        public int FacebookId
        {
            get { return Id; }
            set { Id = value;  }

        }
    }
}
