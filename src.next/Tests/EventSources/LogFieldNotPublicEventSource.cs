using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotPublic")]
    public sealed class LogFieldNotPublicEventSource
        : EventSource
    {
        internal static LogFieldNotPublicEventSource Log = new LogFieldNotPublicEventSource();
    }
}