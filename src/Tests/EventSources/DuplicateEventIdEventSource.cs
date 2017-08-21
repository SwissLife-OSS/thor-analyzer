using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "DuplicateEventId")]
    public sealed class DuplicateEventIdEventSource
        : EventSource
        , IDuplicateEventIdEventSource
    {
        public static IDuplicateEventIdEventSource Log = new DuplicateEventIdEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(1)]
        public void Bar(string foo)
        {
            WriteEvent(1, foo);
        }

        [Event(2)]
        public void Valid()
        {
            WriteEvent(2);
        }
    }

    public interface IDuplicateEventIdEventSource
    {
        void Foo(string bar);

        void Bar(string foo);

        void Valid();
    }
}