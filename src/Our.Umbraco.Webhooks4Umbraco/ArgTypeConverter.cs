using System.ComponentModel;

namespace Our.Umbraco.Webhooks4Umbraco
{
    public abstract class ArgTypeConverter : TypeConverter
    {
        public abstract string Key { get; }
    }
}