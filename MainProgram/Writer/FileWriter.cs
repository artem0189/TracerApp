using System.IO;
using TracerLib;
using MainProgram.Serialization;

namespace MainProgram.Writer
{
    public static class FileWriter
    {
        public static void Output(ISerialization serializer, TraceResult result)
        {
            string text = serializer.Serialize<TraceResult>(result);
            File.WriteAllText("result" + serializer.Extension, text);
        }
    }
}
