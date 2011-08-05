using System.IO;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;


namespace Megastar.AzureWP7Client
{
    public class MegaStarXmlSerializer : IDeserializer
    {
        private string _namespace;
        private string _dateFormat;


        public T Deserialize<T>(RestResponse response) where T : new()
        {
            var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof (T));

            T result;

            try
            {
                result = (T) xmlSer.Deserialize(new StringReader(response.Content));
            }
            catch
            {
                return new T();
            }

            return result;
        }


        public string RootElement { get; set; }

        public string Namespace { get; set; }

        public string DateFormat { get; set; }


    }
}
