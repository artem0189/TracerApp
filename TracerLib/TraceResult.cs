using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TracerLib
{
    public class TraceResult
    {
        public ConcurrentBag<ThreadInfo> Threads { get; }
        internal ThreadInfo _currentThread;

        internal TraceResult()
        {
            Threads = new ConcurrentBag<ThreadInfo>();
        }

        internal void AddMethodIndo(int threadId, MethodBase methodInfo)
        {
            if (Threads.Where(elem => elem.Id == threadId).Count() == 0)
            {
                _currentThread = new ThreadInfo(threadId);
                Threads.Add(_currentThread);
            }

            MethodInfo newMethod = new MethodInfo(methodInfo.Name, methodInfo.DeclaringType.Name, _currentThread.CurrentElement);
            _currentThread.CurrentElement.Methods.Add(newMethod);
            _currentThread.CurrentElement = _currentThread.CurrentElement.Methods[_currentThread.CurrentElement.Methods.Count - 1];
        }

        internal void SetTimeAndGoToParent(int threadId, double time)
        {
            _currentThread = Threads.Where(elem => elem.Id == threadId).First();
            _currentThread.CurrentElement.Time = time;
            _currentThread.Time += time;
            _currentThread.CurrentElement = _currentThread.CurrentElement.Parent;
        }
    }
}
