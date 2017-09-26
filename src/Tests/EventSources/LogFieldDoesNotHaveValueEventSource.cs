using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotHaveValue")]
    public sealed class LogFieldDoesNotHaveValueEventSource
        : EventSource
    {
        public static readonly LogFieldDoesNotHaveValueEventSource Log;
    }
}