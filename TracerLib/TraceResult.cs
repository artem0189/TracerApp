using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace TracerLib
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, ThreadInfo> Threads { get; }
        internal ThreadInfo _currentThread;

        internal TraceResult()
        {
            Threads = new ConcurrentDictionary<int, ThreadInfo>();
        }

        internal void AddMethodIndo(int threadId, MethodBase methodInfo)
        {
            _currentThread = Threads.GetOrAdd(threadId, new ThreadInfo(threadId));
            MethodInfo newMethod = new MethodInfo(methodInfo.Name, methodInfo.DeclaringType.Name, _currentThread.CurrentElement);
            newMethod.Time = (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
            _currentThread.CurrentElement.Methods.Add(newMethod);
            _currentThread.CurrentElement = _currentThread.CurrentElement.Methods[_currentThread.CurrentElement.Methods.Count - 1];
        }

        internal void CloseMethodInfo(int threadId)
        {
            if (Threads.TryGetValue(threadId, out _currentThread))
            {
                _currentThread.CurrentElement.Time = (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds - _currentThread.CurrentElement.Time;
                _currentThread.Time += _currentThread.CurrentElement.Time;
                _currentThread.CurrentElement = _currentThread.CurrentElement.Parent;
            }
        }
    }
}
