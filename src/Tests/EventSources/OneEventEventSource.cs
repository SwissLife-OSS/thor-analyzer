using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "OneEvent")]
    public sealed class OneEventEventSource
        : EventSource
    {
        private OneEventEventSource() { }

        public static OneEventEventSource Log = new OneEventEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}