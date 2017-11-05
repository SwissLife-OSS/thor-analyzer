using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotHaveValue")]
    public sealed class LogFieldDoesNotHaveValueEventSource
        : EventSource
    {
        public static readonly LogFieldDoesNotHaveValueEventSource Log;
    }
}