using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Serialization
{
    public class SerializerXml : ISerialization
    {
        public string Serialize<T>(T obj)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true };
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            using (StringWriter strWriter = new StringWriter())
            using (XmlWriter xmlWriter = XmlWriter.Create(strWriter, settings))
            {
                formatter.Serialize(xmlWriter, obj, namespaces);
                return strWriter.ToString();
            }
        }
    }
}
