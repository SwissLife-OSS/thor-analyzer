using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "EventWorkingWithDefaults")]
    public sealed class EventWorkingWithDefaultsEventSource
        : EventSource
    {
        private EventWorkingWithDefaultsEventSource() { }

        public static EventWorkingWithDefaultsEventSource Log = new EventWorkingWithDefaultsEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}