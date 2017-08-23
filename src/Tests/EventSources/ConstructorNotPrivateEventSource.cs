using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorNotPrivate")]
    public sealed class ConstructorNotPrivateEventSource
        : EventSource
    {
        public ConstructorNotPrivateEventSource() { }
    }
}