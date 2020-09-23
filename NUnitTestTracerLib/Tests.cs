using System.Threading;
using NUnit.Framework;
using TracerLib;

namespace NUnitTestTracerLib
{
    public class Tests
    {
        private Tracer _tracer;

        [SetUp]
        public void Setup()
        {
            _tracer = new Tracer();
        }

        [Test]
        public void ThreadCount()
        {
            const int threadsCount = 5;

            for (int i = 0; i < threadsCount; i++)
            {
                Thread thread = new Thread(new ThreadStart(new CheckCount(_tracer).Method));
                thread.Start();
                thread.Join();
            }

            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.AreEqual(threadsCount, traceResult.Threads.Count);
        }

        [Test]
        public void MethodCount()
        {
            const int methodsCount = 5;

            CheckCount checkCount = new CheckCount(_tracer);
            for (int i = 0; i < methodsCount; i++)
            {
                checkCount.Method();
            }
            TraceResult traceResult = _tracer.GetTraceResult();

            Assert.AreEqual(methodsCount, traceResult.Threads[0].Methods.Count);
        }

        [Test]
        public void MethodName()
        {
            (new CheckCount(_tracer)).Method();
            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.AreEqual("CheckCount", traceResult.Threads[0].Methods[0].ClassName);
            Assert.AreEqual("Method", traceResult.Threads[0].Methods[0].Name);
        }

        [Test]
        public void RecursionCount()
        {
            const int recursionsCount = 3;

            (new CheckRecursionCount(_tracer, recursionsCount)).Method();
            TraceResult traceResult = _tracer.GetTraceResult();

            MethodInfo methodInfo = traceResult.Threads[0].Methods[0];
            for (int i = 0; i < recursionsCount; i++)
            {
                Assert.AreEqual("CheckRecursionCount", methodInfo.ClassName);
                Assert.AreEqual("Method", methodInfo.Name);
                methodInfo = methodInfo.Methods[0];
            }
            Assert.AreEqual(0, methodInfo.Methods.Count);
        }

        [Test]
        public void MethodTime()
        {
            const int methodTime = 5000;

            (new CheckTime(_tracer, methodTime)).Method();
            TraceResult traceResult = _tracer.GetTraceResult();

            Assert.IsTrue(methodTime <= traceResult.Threads[0].Methods[0].Time);
        }

        [Test]
        public void ThreadTime()
        {
            const int methodTime = 3000;
            const int methodCount = 3;

            CheckTime checkTime = new CheckTime(_tracer, methodTime);

            for (int i = 0; i < methodCount; i++)
            {
                checkTime.Method();
            }
            TraceResult traceResult = _tracer.GetTraceResult();

            Assert.IsTrue(methodCount * methodTime <= traceResult.Threads[0].Time);
        }
    }

    public class CheckCount
    {
        private Tracer _tracer;

        public CheckCount(Tracer tracer)
        {
            _tracer = tracer;
        }

        public void Method()
        {
            _tracer.StartTrace();
            _tracer.StopTrace();
        }
    }

    public class CheckRecursionCount
    {
        private Tracer _tracer;
        private int _count; 

        public CheckRecursionCount(Tracer tracer, int count)
        {
            _tracer = tracer;
            _count = count;
        }

        public void Method()
        {
            _tracer.StartTrace();

            if (_count > 0)
            {
                _count--;
                Method();
            }

            _tracer.StopTrace();
        }
    }

    public class CheckTime
    {
        private Tracer _tracer;
        private int _sleepTime;

        public CheckTime(Tracer tracer, int sleepTime)
        {
            _tracer = tracer;
            _sleepTime = sleepTime;
        }

        public void Method()
        {
            _tracer.StartTrace();

            Thread.Sleep(_sleepTime);

            _tracer.StopTrace();
        }
    }
}