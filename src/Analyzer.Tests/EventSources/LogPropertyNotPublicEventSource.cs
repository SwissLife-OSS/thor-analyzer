using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogPropertyNotPublic")]
    public sealed class LogPropertyNotPublicEventSource
        : EventSource
    {
        internal static LogPropertyNotPublicEventSource Log { get; } =
            new LogPropertyNotPublicEventSource();
    }
}