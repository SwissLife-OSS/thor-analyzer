using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorNotPrivate")]
    public sealed class ConstructorNotPrivateEventSource
        : EventSource
    {
        public ConstructorNotPrivateEventSource() { }
    }
}