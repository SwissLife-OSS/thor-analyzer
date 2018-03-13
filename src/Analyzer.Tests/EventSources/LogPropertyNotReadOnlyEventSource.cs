using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogPropertyNotReadOnly")]
    public sealed class LogPropertyNotReadOnlyEventSource
        : EventSource
    {
        public static LogPropertyNotReadOnlyEventSource Log { get; set; } =
            new LogPropertyNotReadOnlyEventSource();
    }
}