using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Sealed")]
    public sealed class SealedEventSource
        : EventSource
    {
    }
}