using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private TraceResult _tracerResult;

        public Tracer()
        {
            _tracerResult = new TraceResult();
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodInfo = stackTrace.GetFrame(1).GetMethod();
            _tracerResult.AddMethodIndo(Thread.CurrentThread.ManagedThreadId, methodInfo);
        }

        public void StopTrace()
        {
            _tracerResult.CloseMethodInfo(Thread.CurrentThread.ManagedThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
