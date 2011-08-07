using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class SongResponse
    {
        [DataMember]
        public int id;

        [DataMember]
        public string name;

        [DataMember]
        public string artistName;

        [DataMember] 
        public int length;

        [DataMember] 
        public string playUrl;

    }
}
