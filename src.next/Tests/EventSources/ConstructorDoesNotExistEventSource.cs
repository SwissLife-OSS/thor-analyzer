using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorDoesNotExist")]
    public sealed class ConstructorDoesNotExistEventSource
        : EventSource
    {
    }
}