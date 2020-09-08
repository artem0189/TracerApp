using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace TracerLib
{
    public class MethodInfo : IElementInfo
    {
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("class")]
        public string ClassName { get; }
        [JsonProperty("time")]
        public double Time { get; set; }
        [JsonIgnore]
        public IElementInfo Parent { get; }
        [JsonProperty("methods")]
        public List<MethodInfo> Methods { get; } 

        public MethodInfo(string name, string className, IElementInfo parent)
        {
            Name = name;
            ClassName = className;
            Parent = parent;
            Methods = new List<MethodInfo>();
        }
    }
}
