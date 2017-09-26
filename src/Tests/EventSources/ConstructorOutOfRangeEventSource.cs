using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "ConstructorOutOfRange")]
    public sealed class ConstructorOutOfRangeEventSource
        : EventSource
    {
        private ConstructorOutOfRangeEventSource(string test) { }

        public static readonly ConstructorOutOfRangeEventSource Log =
            new ConstructorOutOfRangeEventSource("test");
    }
}