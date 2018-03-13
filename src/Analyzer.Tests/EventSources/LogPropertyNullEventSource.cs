using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogPropertyNull")]
    public sealed class LogPropertyNullEventSource
        : EventSource
    {
        public static LogPropertyNullEventSource Log { get; }
    }
}