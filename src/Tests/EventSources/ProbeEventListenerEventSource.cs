using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ProbeEventListener")]
    public sealed class ProbeEventListenerEventSource
       : EventSource
    {
        public static readonly ProbeEventListenerEventSource Log = new ProbeEventListenerEventSource();

        [Event(1)]
        public void Foo(string foo)
        {
            WriteEvent(1, foo);
        }

        [Event(2)]
        public void Bar(string bar)
        {
            WriteEvent(2, bar);
        }
    }
}