using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "LogFieldNotStatic")]
    public sealed class LogFieldNotStaticEventSource
        : EventSource
    {
        public LogFieldNotStaticEventSource Log;
    }
}