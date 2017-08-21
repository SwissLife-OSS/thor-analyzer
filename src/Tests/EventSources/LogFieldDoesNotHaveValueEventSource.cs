using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotHaveValue")]
    public sealed class LogFieldDoesNotHaveValueEventSource
        : EventSource
    {
        public static LogFieldDoesNotHaveValueEventSource Log;
    }
}