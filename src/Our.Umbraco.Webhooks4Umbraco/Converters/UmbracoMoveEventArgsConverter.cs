using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Our.Umbraco.Webhooks4Umbraco.Extensions;
using Umbraco.Core.Events;
using Umbraco.Core.Models;

namespace Our.Umbraco.Webhooks4Umbraco.Converters
{
    public abstract class UmbracoMoveEventArgsConverter<TEntity> : ArgTypeConverter
    {
        public override string Key
        {
            get
            {
                return "movedEventArgs";
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(MoveEventArgs<TEntity>);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var typedValue = (MoveEventArgs<TEntity>)value;
            if (typedValue == null) return null;

            return new
            {
                entities = typedValue.MoveInfoCollection.Select(x => new
                {
                    entity = this.ConvertFrom(context, culture, x.Entity),
                    newParentId = x.NewParentId,
                    originalPath = x.OriginalPath
                })
            };
        }

        public abstract object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, TEntity value);
    }

    public class UmbracoContentMoveEventArgsConverter : UmbracoMoveEventArgsConverter<IContent>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IContent value)
        {
            return value.ToSerializableObject();
        }
    }

    public class UmbracoMediaMoveEventArgsConverter : UmbracoMoveEventArgsConverter<IMedia>
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, IMedia value)
        {
            return value.ToSerializableObject();
        }
    }
}