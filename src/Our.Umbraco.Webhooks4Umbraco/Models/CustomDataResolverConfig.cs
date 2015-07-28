using System.Collections.Generic;
using System.Xml.Serialization;

using Newtonsoft.Json;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    [XmlType("CustomDataResolver")]
    public class CustomDataResolverConfig
    {
        [JsonProperty("type")]
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}
