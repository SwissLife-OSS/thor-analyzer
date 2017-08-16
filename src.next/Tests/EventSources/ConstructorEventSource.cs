using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Constructor")]
    public sealed class ConstructorEventSource
        : EventSource
    {
        private ConstructorEventSource() { }

        public static ConstructorEventSource Log = new ConstructorEventSource();
    }
}