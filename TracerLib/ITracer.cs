using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib
{
    interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TracerResult GetTraceResult();
    }
}
