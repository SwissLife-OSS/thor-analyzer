using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorDoesNotExist")]
    public sealed class ConstructorDoesNotExistEventSource
        : EventSource
    {
    }
}