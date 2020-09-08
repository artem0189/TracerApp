using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    class ThreadInfo : IElementInfo
    {
        public int Id { get; }
        public double Time { get; set; }
        public List<MethodInfo> Methods { get; }

        public ThreadInfo(int id)
        {
            Id = id;
            Methods = new List<MethodInfo>();
        }
    }
}
