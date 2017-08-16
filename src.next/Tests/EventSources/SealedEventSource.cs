using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Sealed")]
    public sealed class SealedEventSource
        : EventSource
    {
    }
}