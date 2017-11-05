using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "MultipleConstructors")]
    public sealed class MultipleConstructorsEventSource
        : EventSource
    {
        private MultipleConstructorsEventSource() { }

        private MultipleConstructorsEventSource(string test) { }

        public static readonly MultipleConstructorsEventSource Log =
            new MultipleConstructorsEventSource();
    }
}