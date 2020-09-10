using System;
using System.Threading;
using TracerLib;
using Serialization;

namespace MainProgram
{
    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();
            
            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(50);
            InnerMethod2();

            _tracer.StopTrace();
        }

        public void InnerMethod2()
        {
            _tracer.StartTrace();

            Thread.Sleep(100);

            _tracer.StopTrace();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();

            Thread thread1 = new Thread(new ThreadStart(new Foo(tracer).MyMethod));
            thread1.Start();
            thread1.Join();

            new Foo(tracer).MyMethod();

            Console.WriteLine(new SerializerXml().Serialize<TraceResult>(tracer.GetTraceResult()));
        }
    }
}
