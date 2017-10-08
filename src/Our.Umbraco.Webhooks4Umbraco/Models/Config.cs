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
                throw new ApplicationException("The configuration does not have a file path to save to. Please set the `FilePath` property.");

            this.XmlSerialize(FilePath);
        }
    }
}