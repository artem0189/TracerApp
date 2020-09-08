using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    public interface IElementInfo
    {
        double Time { get; set; }
        List<MethodInfo> Methods { get; }
        IElementInfo Parent { get; }
    }
}
