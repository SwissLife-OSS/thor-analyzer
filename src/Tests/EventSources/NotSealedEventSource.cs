using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NotSealed")]
    public class NotSealedEventSource
        : EventSource
    {
    }
}