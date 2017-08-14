using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests
{
    [EventSource(Name = "MultipleEvents")]
    public sealed class MultipleEventsEventSource
       : EventSource
    {
        public static MultipleEventsEventSource Log = new MultipleEventsEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(3, Keywords = Keywords.Diagnostic)]
        public void WithKeywords(string bar)
        {
            WriteEvent(3, bar);
        }

        [Event(4, Level = EventLevel.Verbose)]
        public void WithLevel(string bar)
        {
            WriteEvent(4, bar);
        }

        [Event(6, Opcode = EventOpcode.Receive)]
        public void WithOpcode(string bar)
        {
            WriteEvent(6, bar);
        }

        [Event(7, Tags = (EventTags)5)]
        public void WithTags(string bar)
        {
            WriteEvent(7, bar);
        }

        [Event(8, Task = (EventTask)7)]
        public void WithTask(string bar)
        {
            WriteEvent(8, bar);
        }

        public class Keywords
        {
            public const EventKeywords Diagnostic = (EventKeywords)1;
        }
    }
}