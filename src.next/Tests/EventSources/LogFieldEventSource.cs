using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogField")]
    public sealed class LogFieldEventSource
        : EventSource
    {
        public static LogFieldEventSource Log = new LogFieldEventSource();
    }
}