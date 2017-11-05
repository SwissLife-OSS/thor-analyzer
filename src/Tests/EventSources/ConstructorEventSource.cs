using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Constructor")]
    public sealed class ConstructorEventSource
        : EventSource
    {
        private ConstructorEventSource() { }

        public static readonly ConstructorEventSource Log = new ConstructorEventSource();
    }
}