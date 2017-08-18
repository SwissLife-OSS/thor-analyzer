using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "EventNotWorking")]
    public sealed class EventNotWorkingEventSource
       : EventSource
    {
        private EventNotWorkingEventSource() { }

        public static EventNotWorkingEventSource Log = new EventNotWorkingEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
        }
    }
}