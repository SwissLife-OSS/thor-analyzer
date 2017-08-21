using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NotSealed")]
    public class NotSealedEventSource
        : EventSource
    {
    }
}