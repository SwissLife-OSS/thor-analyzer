using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "1nvalid_Name")]
    public sealed class InvalidNameEventSource
        : EventSource
    {
        public static readonly InvalidNameEventSource Log = new InvalidNameEventSource();

        private InvalidNameEventSource() { }
    }
}