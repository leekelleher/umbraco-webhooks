using System;
using System.ComponentModel;
using System.Globalization;
using Our.Umbraco.Webhooks4Umbraco.Extensions;
using Umbraco.Core.Events;
using Umbraco.Core.Models;

namespace Our.Umbraco.Webhooks4Umbraco.Converters
{
    public abstract class UmbracoRollbackEventArgsConverter<TEntity> : ArgTypeConverter
    {
        public override string Key
        {
            get
            {
                return "rolledBackEventArgs";
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(RollbackEventArgs<TEntity>);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var typedValue = (RollbackEventArgs<TEntity>)value;
            if (typedValue == null) return null;

            return new
            {
                entity = this.ConvertFrom(context, culture, typedValue.Entity)
            };
        }

        public abstract object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, TEntity value);
    }

    public class UmbracoContentRollbackEventArgsConverter : UmbracoRollbackEventArgsConverter<IContent>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IContent value)
        {
            return value.ToSerializableObject();
        }
    }

    public class UmbracoMediaRollbackEventArgsConverter : UmbracoRollbackEventArgsConverter<IMedia>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IMedia value)
        {
            return value.ToSerializableObject();
        }
    }
}