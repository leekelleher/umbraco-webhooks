namespace Our.Umbraco.Webhooks4Umbraco
{
    public abstract class CustomDataResolver
    {
        public abstract string Key { get; }

        public abstract object ResolveData();
    }
}