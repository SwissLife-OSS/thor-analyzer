using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Sealed")]
    public sealed class SealedEventSource
        : EventSource
    {
    }
}