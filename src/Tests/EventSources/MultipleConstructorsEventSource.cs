using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "MultipleConstructors")]
    public sealed class MultipleConstructorsEventSource
        : EventSource
    {
        private MultipleConstructorsEventSource() { }

        private MultipleConstructorsEventSource(string test) { }

        public static MultipleConstructorsEventSource Log = new MultipleConstructorsEventSource();
    }
}