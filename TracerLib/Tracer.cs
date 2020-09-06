using System;
using System.Collections.Generic;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private Stack<int> _timeStack;
        private TracerResult _tracerResult;

        public Tracer()
        {
            _timeStack = new Stack<int>();
            _tracerResult = new TracerResult();
        }

        public void StartTrace()
        {
            _tracerResult.AddNode(System.Threading.Thread.CurrentThread.ManagedThreadId, "1", "2");
            _timeStack.Push(DateTime.Now.Millisecond);
        }

        public void StopTrace()
        {
            int startTimeMillisecond = _timeStack.Pop();
            _tracerResult.SetTimeForNode(DateTime.Now.Millisecond - startTimeMillisecond);
        }

        public TracerResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
