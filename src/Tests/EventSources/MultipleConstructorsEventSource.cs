using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
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