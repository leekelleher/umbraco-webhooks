using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    [XmlType("ArgConverter")]
    public class ArgConverterConfig
    {
        [JsonProperty("type")]
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
