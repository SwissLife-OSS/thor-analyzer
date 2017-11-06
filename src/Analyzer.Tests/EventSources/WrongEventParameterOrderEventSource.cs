using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "WrongEventParameterOrder")]
    public sealed class WrongEventParameterOrderEventSource
        : EventSource
    {
        private WrongEventParameterOrderEventSource() { }

        public static readonly WrongEventParameterOrderEventSource Log =
            new WrongEventParameterOrderEventSource();

        [Event(1)]
        public void Foo(string foo, int bar)
        {
            WriteEvent(1, bar, foo);
        }
    }
}