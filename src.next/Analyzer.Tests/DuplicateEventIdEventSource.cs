using Microsoft.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Tests
{
    [EventSource(Name = "DuplicateEventId")]
    public class DuplicateEventIdEventSource
       : EventSource
    {
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
}
