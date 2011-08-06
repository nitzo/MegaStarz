using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class UploadRecordingResponse
    {
        [DataMember]
        public string id;

        [DataMember]
        public string playUrl;
    }
}
