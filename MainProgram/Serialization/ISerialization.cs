using System;

namespace MainProgram.Serialization
{
    public interface ISerialization
    {
        string Serialize<T>(T obj);
        string Extension { get; }
    }
}
