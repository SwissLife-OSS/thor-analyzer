using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogProperty")]
    public sealed class LogPropertyEventSource
        : EventSource
    {
        public static LogPropertyEventSource Log { get; } = new LogPropertyEventSource();
    }
}