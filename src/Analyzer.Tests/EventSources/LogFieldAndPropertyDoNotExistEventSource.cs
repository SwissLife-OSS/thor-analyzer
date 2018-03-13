using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldAndPropertyDoesNotExist")]
    public sealed class LogFieldAndPropertyDoNotExistEventSource
        : EventSource
    {
    }
}