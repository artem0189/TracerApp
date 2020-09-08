using System;
using System.Collections.Generic;
using TracerLib.TreeElement;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private Stack<DateTime> _timeStack;
        private TracerResult _tracerResult;

        public Tracer()
        {
            _timeStack = new Stack<DateTime>();
            _tracerResult = new TracerResult();
        }

        public void StartTrace()
        {
            _tracerResult.AddMethodInfo(System.Threading.Thread.CurrentThread.ManagedThreadId, "1", "2");
            _timeStack.Push(DateTime.Now);
        }

        public void StopTrace()
        {
            TimeSpan interval = DateTime.Now - _timeStack.Pop();
            _tracerResult.SetMethodTime(interval.TotalMilliseconds);
        }

        public TracerResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
