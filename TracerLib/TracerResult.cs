using System.Linq;
using System.Collections.Generic;
using TracerLib.TreeElement;

namespace TracerLib
{
    public class TracerResult
    {
        private List<ThreadNode> _threads;
        public List<ThreadNode> Threads
        {
            get
            {
                return _threads;
            }
        }

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

        internal void AddMethodInfo(int threadId, string name, string className)
        {
            if (_threads.Where(node => node.Id == threadId).Count() == 0)
            {
                _currentNode = new ThreadNode(threadId, null);
                _threads.Add(_currentNode as ThreadNode);
            }

            _currentNode.Methods.Add(new MethodNode(name, className, _currentNode));
            _currentNode = _currentNode.Methods[_currentNode.Methods.Count - 1];
        }

        internal void SetMethodTime(double time)
        {
            _currentNode.Time = time;
            _currentNode = _currentNode.PreviousNode; //?
        }
    }
}
