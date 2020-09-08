using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace TracerLib
{
    public class ThreadInfo : IElementInfo
    {
        [JsonProperty("id")]
        public int Id { get; }
        [JsonProperty("time")]
        public double Time { get; set; }
        [JsonIgnore]
        public IElementInfo Parent { get; }
        [JsonProperty("methods")]
        public List<MethodInfo> Methods { get; }
        [JsonIgnore]
        public IElementInfo CurrentElement { get; set; }

        public ThreadInfo(int id)
        {
            Id = id;
            Parent = null;
            Methods = new List<MethodInfo>();
            CurrentElement = this;
        }
    }
}
