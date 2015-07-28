using System.Configuration;
using System.Web;

using Our.Umbraco.Webhooks4Umbraco;

namespace Pegasus.Webhooks
{
    public class PegasusDataResovler : CustomDataResolver
    {
        public override string Key
        {
            get { return "pegasus"; }
        }

        public override object ResolveData()
        {
            return new {
                siteId = ConfigurationManager.AppSettings["Pegasus:DashboardSiteId"],
                hostName = HttpContext.Current.Request.Url.Host
            };
        }
    }
}
