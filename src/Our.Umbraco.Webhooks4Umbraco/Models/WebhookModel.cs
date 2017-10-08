using System.Collections.Generic;
using Newtonsoft.Json;

namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    public class WebhookModel
    {
        [JsonProperty("event")]
        public EventConfig Event { get; internal set; }

        [JsonProperty("args")]
        public List<object> Args { get; internal set; }

        public WebhookModel()
        {
            Args = new List<object>();
        }
    }
}