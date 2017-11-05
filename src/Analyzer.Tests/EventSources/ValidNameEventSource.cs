using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Valid-Name")]
    public sealed class ValidNameEventSource
        : EventSource
    {
        public static readonly ValidNameEventSource Log = new ValidNameEventSource();

        private ValidNameEventSource() { }
    }
}