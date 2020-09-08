using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    internal interface IElementInfo
    {
        double Time { get; set; }
        List<MethodInfo> Methods { get; }
    }
}
