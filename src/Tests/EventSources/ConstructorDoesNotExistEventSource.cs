using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorDoesNotExist")]
    public sealed class ConstructorDoesNotExistEventSource
        : EventSource
    {
    }
}