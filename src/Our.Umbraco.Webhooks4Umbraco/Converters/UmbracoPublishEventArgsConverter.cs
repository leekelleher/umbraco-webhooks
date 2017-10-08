using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Our.Umbraco.Webhooks4Umbraco.Extensions;
using Umbraco.Core.Events;
using Umbraco.Core.Models;

namespace Our.Umbraco.Webhooks4Umbraco.Converters
{
    public abstract class UmbracoPublishEventArgsConverter<TEntity> : ArgTypeConverter
    {
        public override string Key
        {
            get
            {
                return "publishedEventArgs";
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(PublishEventArgs<TEntity>);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var typedValue = (PublishEventArgs<TEntity>)value;
            if (typedValue == null) return null;

            return new
            {
                entities = typedValue.PublishedEntities.Select(x => this.ConvertFrom(context, culture, x))
            };
        }

        public abstract object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, TEntity value);
    }

    public class UmbracoContentPublishEventArgsConverter : UmbracoPublishEventArgsConverter<IContent>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IContent value)
        {
            return value.ToSerializableObject();
        }
    }

    public class UmbracoMediaPublishEventArgsConverter : UmbracoPublishEventArgsConverter<IMedia>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IMedia value)
        {
            return value.ToSerializableObject();
        }
    }
}