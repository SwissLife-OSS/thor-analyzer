using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorStatic")]
    public sealed class ConstructorStaticEventSource
        : EventSource
    {
        static ConstructorStaticEventSource() { }
    }
}