using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests
{
    [EventSource(Name = "OneEvent")]
    public class OneEventEventSource
        : EventSource
    {
        public static OneEventEventSource Log = new OneEventEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}