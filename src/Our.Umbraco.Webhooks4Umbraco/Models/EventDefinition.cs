namespace Our.Umbraco.Webhooks4Umbraco.Models
{
    public class EventDefinition
    {
        public string FullName { get; set; }
        public string EventName { get; set; }
        public string TypeName { get; set; }
        public string AssemblyName { get; set; }
        public string AssemblyQualifiedTypeName { get; set; }
    }
}