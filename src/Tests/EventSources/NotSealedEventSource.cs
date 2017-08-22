using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NotSealed")]
    public class NotSealedEventSource
        : EventSource
    {
    }
}