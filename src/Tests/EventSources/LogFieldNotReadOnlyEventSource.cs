using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotReadOnly")]
    public sealed class LogFieldNotReadOnlyEventSource
        : EventSource
    {
        public static LogFieldNotReadOnlyEventSource Log = new LogFieldNotReadOnlyEventSource();
    }
}