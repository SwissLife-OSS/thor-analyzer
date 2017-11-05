using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "EventWorking")]
    public sealed class EventWorkingEventSource
        : EventSource
    {
        private EventWorkingEventSource() { }

        public static readonly EventWorkingEventSource Log = new EventWorkingEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}