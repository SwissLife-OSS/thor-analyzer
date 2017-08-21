using System;
using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "CorrectEventParameterOrder")]
    public sealed class CorrectEventParameterOrderEventSource
        : EventSource
    {
        private CorrectEventParameterOrderEventSource() { }

        public static CorrectEventParameterOrderEventSource Log = new CorrectEventParameterOrderEventSource();

        [Event(1)]
        public void Foo(string foo, string bar)
        {
            WriteEvent(1, foo, bar);
        }
    }
}