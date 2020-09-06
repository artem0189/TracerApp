using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.TreeElement
{
    public class MethodNode : INode
    {
        public string Name { get; }
        public string Class { get; }
        public int Time { get; set; }
        public List<INode> Methods { get; }
        public INode PreviousNode { get; }

        public MethodNode(string name, string className, INode previousNode)
        {
            Name = name;
            Class = className;
            Methods = new List<INode>();
            PreviousNode = previousNode;
        }
    }
}
