using System;
using System.Threading;
using TracerLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

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

            _tracer.StopTrace();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();
            new Foo(tracer).MyMethod();
            new Foo(tracer).MyMethod();

            Thread thr1 = new Thread(new ThreadStart(new Bar(tracer).InnerMethod));
            thr1.Start();
            thr1.Join();

            string json = JsonConvert.SerializeObject(tracer.GetTraceResult(), Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}
