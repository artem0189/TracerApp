using System.Linq;
using System.Collections.Generic;
using TracerLib.TreeElement;

namespace TracerLib
{
    public class TracerResult
    {
        private List<ThreadNode> _threads;
        private INode _currentNode;

        internal TracerResult()
        {
            _threads = new List<ThreadNode>();
        }

        public INode this[int index]
        {
            get
            {
                return _threads[index];
            }
        }

        internal void AddMethodInfo(int threadId, INode newNode)
        {
            if (_threads.Where(node => node.Id == threadId).Count() == 0)
            {
                _currentNode = new ThreadNode(threadId, null);
                _threads.Add(_currentNode as ThreadNode);
            }

            _currentNode.Methods.Add(newNode);
            _currentNode = _currentNode.Methods[_currentNode.Methods.Count - 1];
        }

        internal void SetMethodTime(int time)
        {
            _currentNode.Time = time;
            _currentNode = _currentNode.PreviousNode; //?
        }
    }
}
