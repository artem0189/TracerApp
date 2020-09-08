using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    class MethodInfo : IElementInfo
    {
        public string Name { get; }
        public string ClassName { get; }
        public double Time { get; set; }
        public IElementInfo Parent { get; }
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
