using System;

namespace TracerLib
{
    public class Tracer : ITracer
    {
        private TracerResult _tracerResult;
        public Tracer()
        {
            _tracerResult = new TracerResult();
        }
        public void StartTrace()
        {

        }

        public void StopTrace()
        {

        }

        public TracerResult GetTraceResult()
        {
            return _tracerResult;
        }
    }
}
