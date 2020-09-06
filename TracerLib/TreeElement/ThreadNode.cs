using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.TreeElement
{
    public class ThreadNode : INode
    {
        public int Id { get; }
        public int Time { get; set; }
        public List<INode> Methods { get; }
        public INode PreviousNode { get; }

        public ThreadNode(int id, INode previousNode)
        {
            Id = id;
            Methods = new List<INode>();
            PreviousNode = previousNode;
        }
    }
}
