using Our.Umbraco.Webhooks4Umbraco.Servcies;

using Umbraco.Core;

namespace Our.Umbraco.Webhooks4Umbraco
{
    public class Bootstrapper : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, 
            ApplicationContext applicationContext)
        {
            new EventService().AttachEventHandlers();
        }
    }
}
