using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Valid-Name")]
    public sealed class ValidNameEventSource
        : EventSource
    {
        public readonly static ValidNameEventSource Log = new ValidNameEventSource();

        private ValidNameEventSource() { }
    }
}