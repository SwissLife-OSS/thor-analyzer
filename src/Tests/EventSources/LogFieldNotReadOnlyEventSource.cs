using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotReadOnly")]
    public sealed class LogFieldNotReadOnlyEventSource
        : EventSource
    {
        public static LogFieldNotReadOnlyEventSource Log = new LogFieldNotReadOnlyEventSource();
    }
}