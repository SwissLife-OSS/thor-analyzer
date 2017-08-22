using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotExist")]
    public sealed class LogFieldDoesNotExistEventSource
        : EventSource
    {
    }
}