using System;
using TracerLib;
using MainProgram.Serialization;

namespace MainProgram.Writer
{
    public static class ConsoleWriter
    {
        public static void Output(ISerialization serializer, TraceResult result)
        {
            string text = serializer.Serialize<TraceResult>(result);
            Console.WriteLine(text);
        }
    }
}
