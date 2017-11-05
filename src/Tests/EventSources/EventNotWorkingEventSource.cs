using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "EventNotWorking")]
    public sealed class EventNotWorkingEventSource
       : EventSource
    {
        private EventNotWorkingEventSource() { }

        public static readonly EventNotWorkingEventSource Log = new EventNotWorkingEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
        }
    }
}