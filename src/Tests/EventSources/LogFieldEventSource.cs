using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogField")]
    public sealed class LogFieldEventSource
        : EventSource
    {
        public static readonly LogFieldEventSource Log = new LogFieldEventSource();
    }
}