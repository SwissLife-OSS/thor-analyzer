using System;
using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
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
            WriteCore(1, bar, foo);
        }

        [NonEvent]
        private unsafe void WriteCore(int eventId, int bar, string foo)
        {
            if (IsEnabled())
            {
                foo = foo ?? string.Empty;

                fixed (char* fooBytes = foo)
                {
                    const short dataCount = 2;
                    EventData* data = stackalloc EventData[dataCount];

                    data[0].DataPointer = (IntPtr)(&bar);
                    data[0].Size = 4;
                    data[1].DataPointer = (IntPtr)fooBytes;
                    data[1].Size = ((foo.Length + 1) * 2);

                    WriteEventCore(eventId, dataCount, data);
                }
            }
        }
    }
}