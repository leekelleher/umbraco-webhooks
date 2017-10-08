using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    [XmlType("Event")]
    public class EventConfig
    {
        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        [XmlAttribute("type")]
        public string Type { get; set; }

        [JsonIgnore]
        [XmlArray("ArgConverters")]
        public List<ArgConverterConfig> ArgConverters { get; set; }

        public EventConfig()
        {
            ArgConverters = new List<ArgConverterConfig>();
        }
    }
}