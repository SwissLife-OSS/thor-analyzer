using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorStatic")]
    public sealed class ConstructorStaticEventSource
        : EventSource
    {
        static ConstructorStaticEventSource() { }
    }
}