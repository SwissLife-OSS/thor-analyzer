using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogPropertyNotStatic")]
    public sealed class LogPropertyNotStaticEventSource
        : EventSource
    {
        public LogPropertyNotStaticEventSource Log { get; } =
            new LogPropertyNotStaticEventSource();
    }
}