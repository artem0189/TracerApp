using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.TreeElement
{
    class ThreadNode : INode
    {
        public int Id { get; }
        public int Time { get; set; }
        public List<INode> Methods { get; }

        public ThreadNode(int id)
        {
            Id = id;
            Methods = new List<INode>();
        }
    }
}
