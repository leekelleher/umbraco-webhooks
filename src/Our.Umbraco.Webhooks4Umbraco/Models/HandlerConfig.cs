﻿using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    [XmlType("Handler")]
    public class HandlerConfig
    {
        [JsonProperty("webhookUrl")]
        [XmlAttribute("webhookUrl")]
        public string WebhookUrl { get; set; }

        [JsonProperty("webhookUserName")]
        [XmlAttribute("webhookUserName")]
        public string WebhookUserName { get; set; }

        [JsonProperty("webhookPassword")]
        [XmlAttribute("webhookPassword")]
        public string WebhookPassword { get; set; }

        [JsonProperty("webhookCredentials")]
        [XmlAttribute("webhookCredentials")]
        public string WebhookCredentials { get; set; }

        [JsonProperty("events")]
        [XmlArray("Events")]
        public List<EventConfig> Events { get; set; }

        [JsonProperty("customDataResolvers")]
        [XmlArray("CustomDataResolvers")]
        public List<CustomDataResolverConfig> CustomDataResolvers { get; set; }

        public HandlerConfig()
        {
            Events = new List<EventConfig>();
            CustomDataResolvers = new List<CustomDataResolverConfig>();
        }
    }
}