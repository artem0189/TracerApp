using System;
using System.Collections.Generic;
using System.Text;

namespace Serialization
{
    internal interface ISerialization
    {
        string Serialize<T>(T obj);
    }
}
