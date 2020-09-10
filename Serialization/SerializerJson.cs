using System;
using Newtonsoft.Json;

namespace Serialization
{
    public class SerializerJson : ISerialization
    {
        public string Serialize<T>(T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
    }
}
