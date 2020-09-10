using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private TraceResult _tracerResult;
        private ConcurrentDictionary<int, Stack<double>> _stacks;

        public Tracer()
        {
            _tracerResult = new TraceResult();
            _stacks = new ConcurrentDictionary<int, Stack<double>>();
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            MethodBase methodInfo = GetMethodInformation();
            _tracerResult.AddMethodIndo(threadId, methodInfo.Name, methodInfo.DeclaringType.Name);
            _stacks.GetOrAdd(threadId, new Stack<double>()).Push(GetTimeInMilliseconds());
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Stack<double> stack;
            if (_stacks.TryGetValue(threadId, out stack))
            {
                _tracerResult.SetTimeAndGoToParent(threadId, GetTimeInMilliseconds() - stack.Pop());
            }
        }

        private MethodBase GetMethodInformation()
        {
            StackTrace stackTrace = new StackTrace();
            return stackTrace.GetFrame(2).GetMethod();
        }

        private double GetTimeInMilliseconds()
        {
            TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks);
            return timeSpan.TotalMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
