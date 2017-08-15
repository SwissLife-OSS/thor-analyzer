using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldDoesNotExist")]
    public sealed class LogFieldDoesNotExistEventSource
        : EventSource
    {
    }
}