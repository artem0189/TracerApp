using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace TracerLib
{
    [Serializable]
    [XmlRoot("root")]
    public class TraceResult
    {
        [JsonProperty("threads")]
        [XmlElement("thread")]
        public List<ThreadInfo> Threads { get; }

        internal TraceResult()
        {
            Threads = new List<ThreadInfo>();
        }

        internal void AddMethodIndo(int threadId, string name, string className)
        {
            if (Threads.Where(elem => elem.Id == threadId).Count() == 0)
            {
                Threads.Add(new ThreadInfo(threadId));
            }
            ThreadInfo currentThread = Threads.Where(elem => elem.Id == threadId).First();
            MethodInfo newMethod = new MethodInfo(name, className, currentThread.CurrentElement);
            currentThread.CurrentElement.Methods.Add(newMethod);
            currentThread.CurrentElement = currentThread.CurrentElement.Methods[currentThread.CurrentElement.Methods.Count - 1];
        }

        internal void SetTimeAndGoToParent(int threadId, double time)
        {
            ThreadInfo currentThread = Threads.Where(elem => elem.Id == threadId).First();
            currentThread.CurrentElement.Time = time;
            currentThread.Time += time;
            currentThread.CurrentElement = currentThread.CurrentElement.Parent;
        }
    }
}
