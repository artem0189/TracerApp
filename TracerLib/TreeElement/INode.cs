using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.TreeElement
{
    public interface INode
    {
        int Time { get; set; }
        List<INode> Methods { get; }
        INode PreviousNode { get; }
    }
}
