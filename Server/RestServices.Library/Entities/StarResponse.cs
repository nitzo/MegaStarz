using System.Runtime.Serialization;

namespace Megastar.RestServices.Library.Entities
{
    [DataContract(Namespace = "")]
    public class StarResponse
    {

        [DataMember]
        public string FirstName;

        [DataMember]
        public string LastName;

        [DataMember]
        public string Gender;

        [DataMember]
        public string Locale;

        [DataMember]
        public string Birthday;

        [DataMember]
        public string Email;

        [DataMember]
        public string FacebookId;

    }

}
