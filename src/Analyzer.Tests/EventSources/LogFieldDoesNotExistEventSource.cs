using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotExist")]
    public sealed class LogFieldDoesNotExistEventSource
        : EventSource
    {
    }
}