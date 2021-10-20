using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NonUniqueEventId")]
    public class UniqueEventIdEventSource
        : EventSource
    {
        [Event(11)]
        public void Foo1a(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(12)]
        public void Foo1b(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(21)]
        public void Foo2a(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(22)]
        public void Foo2b(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(23)]
        public void Foo2c(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}