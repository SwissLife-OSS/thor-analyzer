using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotPublic")]
    public sealed class LogFieldNotPublicEventSource
        : EventSource
    {
        internal static readonly LogFieldNotPublicEventSource Log =
            new LogFieldNotPublicEventSource();
    }
}