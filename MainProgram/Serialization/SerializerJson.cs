using Newtonsoft.Json;

namespace MainProgram.Serialization
{
    public class SerializerJson : ISerialization
    {
        public string Extension { get; } = ".json";
        public string Serialize<T>(T obj)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, settings);
        }
    }
}
