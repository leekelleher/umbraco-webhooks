﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Our.Umbraco.Webhooks4Umbraco.Events;
using Our.Umbraco.Webhooks4Umbraco.Models;
using Umbraco.Core.Logging;
using Umbraco.Web;

namespace Our.Umbraco.Webhooks4Umbraco.Services
{
    public class EventService
    {
        private readonly static Lazy<EventService> _instance = new Lazy<EventService>(() => new EventService());

        private Dictionary<string, Delegate> _hooks = new Dictionary<string, Delegate>();

        protected EventService()
        { }

        public static EventService Instance
        {
            get { return _instance.Value; }
        }

        public virtual void AttachEventHandlers()
        {
            foreach (var handler in Webhooks4UmbracoConfig.Instance.Handlers)
            {
                AttachEventHandler(handler);
            }
        }

        public virtual void AttachEventHandler(HandlerConfig handler)
        {
            foreach (var evt in handler.Events)
            {
                var typeInf = Type.GetType(evt.Type);
                if (typeInf == null)
                    continue;

                var evtInf = typeInf.GetEvent(evt.Name);
                if (evtInf == null || _hooks.ContainsKey(evtInf.Name))
                    continue;

                var parameterCount = EventParameterCount(evtInf);
                var proxy = new EventProxy(OnEvent, typeInf, evtInf.Name);
                var hookMethod = EventProxy.OnEventMethodInfos[parameterCount];
                var d = Delegate.CreateDelegate(evtInf.EventHandlerType, proxy, hookMethod);

                _hooks[evtInf.Name] = d;

                evtInf.AddEventHandler(typeInf, d);
            }
        }

        private int EventParameterCount(EventInfo eventInfo)
        {
            // First, from the EventInfo get the delegate type.
            var eventHandlerType = eventInfo.EventHandlerType;
            if (eventHandlerType.BaseType != typeof(MulticastDelegate))
            {
                // Must not have been an event after all?!?
                return 0;
            }

            // The signature we want to look at is the delegate's Invoke method.
            var invoke = eventHandlerType.GetMethod("Invoke");
            if (invoke == null)
            {
                // Must not have been a delegate after all?!?
                return 0;
            }

            // Get the delegate's parameter list...
            var parameters = invoke.GetParameters();
            if (parameters.Length > 10)
            {
                return 0;
            }

            return parameters.Length;
        }

        public void OnEvent(object sender, string eventName, object[] args)
        {
            foreach (var handler in Webhooks4UmbracoConfig.Instance.Handlers)
            {
                var evt = handler.Events.FirstOrDefault(e => e.Name == eventName && ((Type)sender) == Type.GetType(e.Type));
                if (evt == null)
                    continue;

                using (var client = new WebClient())
                {
                    // Convert args if possible
                    var convertedArgs = new Dictionary<string, object>();
                    foreach (var argConverter in evt.ArgConverters)
                    {
                        var argConverterType = Type.GetType(argConverter.Type);
                        if (argConverterType != null && typeof(ArgTypeConverter).IsAssignableFrom(argConverterType))
                        {
                            var converter = (ArgTypeConverter)Activator.CreateInstance(argConverterType);
                            foreach (var arg in args.Where(x => x != null))
                            {
                                if (converter.CanConvertFrom(arg.GetType()))
                                {
                                    var convertedArg = converter.ConvertFrom(arg);
                                    if (convertedArg != null)
                                    {
                                        convertedArgs.Add(converter.Key, convertedArg);
                                    }
                                }
                            }
                        }
                    }

                    // Resolve any custom data
                    var customData = new Dictionary<string, object>();
                    foreach (var resolverDef in handler.CustomDataResolvers)
                    {
                        var resolverType = Type.GetType(resolverDef.Type);
                        if (resolverType != null && typeof(CustomDataResolver).IsAssignableFrom(resolverType))
                        {
                            var resolver = (CustomDataResolver)Activator.CreateInstance(resolverType);
                            customData.Add(resolver.Key, resolver.ResolveData());
                        }
                    }

                    // Get current user info
                    var currentUser = UmbracoContext.Current != null
                        ? UmbracoContext.Current.Security.CurrentUser
                        : null;

                    // Create webhook data object
                    var data = new
                    {
                        @event = evt,
                        user = new
                        {
                            id = currentUser != null ? currentUser.Id : 0,
                            name = currentUser != null ? currentUser.Name : null,
                            email = currentUser != null ? currentUser.Email : null
                        },
                        args = convertedArgs,
                        customData = customData
                    };

                    // Send webhook request
                    var dataString = JsonConvert.SerializeObject(data);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                    // Add credentials - if available
                    var credentials = string.Empty;
                    if (!string.IsNullOrWhiteSpace(handler.WebhookCredentials))
                    {
                        // Assumes the credentials are already Base64 encoded
                        credentials = handler.WebhookCredentials;
                    }
                    else if (!string.IsNullOrWhiteSpace(handler.WebhookUserName) && !string.IsNullOrWhiteSpace(handler.WebhookPassword))
                    {
                        credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Concat(handler.WebhookUserName, ":", handler.WebhookPassword)));
                    }

                    if (!string.IsNullOrWhiteSpace(credentials))
                    {
                        client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);
                    }

                    try
                    {
                        client.UploadString(new Uri(handler.WebhookUrl), "POST", dataString);
                    }
                    catch (WebException ex)
                    {
                        LogHelper.Error<EventService>("There is an error on the webhook end-point.", ex);
                    }
                }
            }
        }
    }
}