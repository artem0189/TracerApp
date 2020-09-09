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
        private ConcurrentDictionary<int, ConcurrentStack<DateTime>> _stacks;

        public Tracer()
        {
            _tracerResult = new TraceResult();
            _stacks = new ConcurrentDictionary<int, ConcurrentStack<DateTime>>();
        }

        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            _stacks.GetOrAdd(threadId, new ConcurrentStack<DateTime>()).Push(DateTime.Now);
            StackTrace stackTrace = new StackTrace();
            MethodBase methodInfo = stackTrace.GetFrame(1).GetMethod();
            _tracerResult.AddMethodIndo(threadId, methodInfo);
        }

        public void StopTrace()
        {
            double time = CalculateTime(Thread.CurrentThread.ManagedThreadId);
            _tracerResult.SetTimeAndGoToParent(Thread.CurrentThread.ManagedThreadId ,time);
        }

        private double CalculateTime(int threadId)
        {
            double timeMilliseconds = 0;
            DateTime stopTime = DateTime.Now;
            DateTime startTime;
            if (_stacks[threadId].TryPop(out startTime))
            {
                timeMilliseconds = (stopTime - startTime).TotalMilliseconds;
            }
            return timeMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
