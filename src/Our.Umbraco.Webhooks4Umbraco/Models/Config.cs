using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Our.Umbraco.Webhooks4Umbraco.Extensions;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    [XmlRoot("Webhooks4Umbraco")]
    public class Config
    {
        [XmlArray("Handlers")]
        public List<HandlerConfig> Handlers { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        internal string FilePath { get; set; }

        internal void Save()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new ApplicationException("Config item has no FilePath to save to.");

            this.XmlSerialize(FilePath);
        }
    }
}
