using System;
using System.Collections.Generic;
using TracerLib.TreeElement;
using System.Text;

namespace TracerLib
{
    public class TracerResult
    {
        private List<INode> _traceTree;
        public TracerResult()
        {
            _traceTree = new List<INode>();
        }

        public void AddNode(INode newNode)
        {

        }
    }
}
