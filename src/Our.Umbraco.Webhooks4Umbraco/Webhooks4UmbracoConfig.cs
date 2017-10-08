using System;
using System.IO;
using Our.Umbraco.Webhooks4Umbraco.Extensions;
using Our.Umbraco.Webhooks4Umbraco.Models;
using Umbraco.Core.IO;

namespace Our.Umbraco.Webhooks4Umbraco
{
    public class Webhooks4UmbracoConfig
    {
        private static readonly Lazy<Config> _instance = new Lazy<Config>(() =>
        {
            var filePath = IOHelper.MapPath(SystemDirectories.Config + "/Webhooks4Umbraco.config");
            var accounts = new FileInfo(filePath).XmlDeserialize<Config>()
                ?? new Config();
            accounts.FilePath = filePath;
            return accounts;
        });

        public static Config Instance
        {
            get { return _instance.Value; }
        }
    }
}