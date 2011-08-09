using System.IO;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;


namespace Megastar.Client.Library
{
    public class MegaStarXmlSerializer : IDeserializer
    {
        private string _namespace;
        private string _dateFormat;


        public T Deserialize<T>(RestResponse response) where T : new()
        {
            return Deserialize<T>(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response.Content)));
        }

        public T Deserialize<T>(string response) where T : new()
        {

            using (var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(response)))
            {
                return Deserialize<T>(ms);
            }
        }

        public T Deserialize<T>(Stream response) where T : new()
        {
            var xmlSer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T result;
            
            try
            {
                result = (T)xmlSer.Deserialize(response);
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
