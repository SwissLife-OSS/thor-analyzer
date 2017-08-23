using System;
using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "CorrectEventParameterOrderWithRelatedActivityId")]
    public sealed class CorrectEventParameterOrderWithRelatedActivityIdEventSource
        : EventSource
    {
        private CorrectEventParameterOrderWithRelatedActivityIdEventSource() { }

        public static CorrectEventParameterOrderWithRelatedActivityIdEventSource Log = new CorrectEventParameterOrderWithRelatedActivityIdEventSource();

        [Event(1, Opcode = EventOpcode.Send)]
        public void Bar(Guid relatedActivityId, string foo, string bar)
        {
            WriteEventWithRelatedActivityId(1, relatedActivityId, foo, bar);
        }
    }
}