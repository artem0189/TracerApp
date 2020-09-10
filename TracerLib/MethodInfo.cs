using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text;

namespace TracerLib
{
    [Serializable]
    public class MethodInfo : IElementInfo
    {
        [JsonProperty("name")]
        [XmlAttribute("name")]
        public string Name { get; set; }

        [JsonProperty("class")]
        [XmlAttribute("class")]
        public string ClassName { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public double Time { get; set; }

        [JsonProperty("time")]
        [XmlAttribute("time")]
        public string StringTime
        {
            get
            {
                return Convert.ToInt32(Time).ToString() + "ms";
            }
            set
            {
                return;
            }
        }

        [JsonIgnore]
        [XmlIgnore]
        public IElementInfo Parent { get; set; }

        [JsonProperty("methods")]
        [XmlElement("method")]
        public List<MethodInfo> Methods { get; set; } 

        public MethodInfo() { }

        public MethodInfo(string name, string className, IElementInfo parent)
        {
            Name = name;
            ClassName = className;
            Parent = parent;
            Methods = new List<MethodInfo>();
        }
    }
}
