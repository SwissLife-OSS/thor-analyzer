using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNull")]
    public sealed class LogFieldNullEventSource
        : EventSource
    {
        public static readonly LogFieldNullEventSource Log;
    }
}