using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NonUniqueEventId")]
    public class NonUniqueEventIdEventSource
        : EventSource
    {
        [Event(1)]
        public void Foo1a(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(1)]
        public void Foo1b(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(2)]
        public void Foo2a(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(2)]
        public void Foo2b(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(2)]
        public void Foo2c(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}