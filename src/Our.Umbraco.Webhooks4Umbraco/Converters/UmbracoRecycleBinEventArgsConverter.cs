using System;
using System.ComponentModel;
using System.Globalization;
using Umbraco.Core.Events;

namespace Our.Umbraco.Webhooks4Umbraco.Converters
{
    public class UmbracoRecycleBinEventArgsConverter : ArgTypeConverter
    {
        public override string Key
        {
            get
            {
                return "emptiedRecycleBinEventArgs";
            }
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(RecycleBinEventArgs);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var typedValue = (RecycleBinEventArgs)value;
            if (typedValue == null) return null;

            return new
            {
                ids = typedValue.Ids,
                isContent = typedValue.IsContentRecycleBin,
                isMedia = typedValue.IsMediaRecycleBin
            };
        }
    }
}