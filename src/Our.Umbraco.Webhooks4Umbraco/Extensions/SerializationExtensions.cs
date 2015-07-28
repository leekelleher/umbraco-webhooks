using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Our.Umbraco.Webhooks4Umbraco.Extensions
{
    internal static class SerializationExtensions
    {
        public static T XmlDeserialize<T>(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return default(T);
            }

            var locker = new object();
            var stringReader = new StringReader(s);
            var reader = new XmlTextReader(stringReader);
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(T));
                lock (locker)
                {
                    var item = (T)xmlSerializer.Deserialize(reader);
                    reader.Close();
                    return item;
                }
            }
            catch
            {
                return default(T);
            }
            finally
            {
                reader.Close();
            }
        }

        public static T XmlDeserialize<T>(this FileInfo fileInfo)
        {
            if (!File.Exists(fileInfo.FullName))
                return default(T);

            var xml = string.Empty;
            using (var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd().XmlDeserialize<T>();
                }
            }
        }

        public static bool XmlSerialize<T>(this T item, string fileName, bool removeNamespaces = true)
        {
            var locker = new object();

            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add(string.Empty, string.Empty);

            var xmlSerializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false
            };

            lock (locker)
            {
                using (var writer = XmlWriter.Create(fileName, settings))
                {
                    if (removeNamespaces)
                    {
                        xmlSerializer.Serialize(writer, item, xmlns);
                    }
                    else
                    {
                        xmlSerializer.Serialize(writer, item);
                    }

                    writer.Close();
                }
            }

            return true;
        }
    }
}
