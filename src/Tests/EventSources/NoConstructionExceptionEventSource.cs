using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "NoConstructionException")]
    public sealed class NoConstructionExceptionEventSource
        : EventSource
    {
        private NoConstructionExceptionEventSource() { }

        public static readonly NoConstructionExceptionEventSource Log =
            new NoConstructionExceptionEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }
    }
}