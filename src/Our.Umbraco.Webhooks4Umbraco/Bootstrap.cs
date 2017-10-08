using Our.Umbraco.Webhooks4Umbraco.Services;
using Umbraco.Core;

namespace Our.Umbraco.Webhooks4Umbraco
{
    internal class Bootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            EventService.Instance.AttachEventHandlers();
        }
    }
}