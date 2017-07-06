using Microsoft.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Tests
{
    [EventSource(Name = "OneEvent")]
    public class OneEventEventSource
        : EventSource
    {
        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}
