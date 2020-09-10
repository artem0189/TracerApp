using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Text;

namespace TracerLib
{
    [Serializable]
    public class ThreadInfo : IElementInfo
    {
        [JsonProperty("id")]
        [XmlAttribute("id")]
        public int Id { get; set; }

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

        [JsonIgnore]
        [XmlIgnore]
        public IElementInfo CurrentElement { get; set; }

        public ThreadInfo() { }

        public ThreadInfo(int id)
        {
            Id = id;
            Parent = null;
            Methods = new List<MethodInfo>();
            CurrentElement = this;
        }
    }
}
