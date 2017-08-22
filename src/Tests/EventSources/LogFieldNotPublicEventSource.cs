using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotPublic")]
    public sealed class LogFieldNotPublicEventSource
        : EventSource
    {
        internal static LogFieldNotPublicEventSource Log = new LogFieldNotPublicEventSource();
    }
}