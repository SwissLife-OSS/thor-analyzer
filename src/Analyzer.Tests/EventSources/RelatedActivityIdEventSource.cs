using System;
using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests.EventSources
{
    [EventSource(Name = "RelatedActivityId")]
    public sealed class RelatedActivityIdEventSource
        : EventSource
    {
        private RelatedActivityIdEventSource() { }

        public static readonly RelatedActivityIdEventSource Log = new RelatedActivityIdEventSource();

        [Event(1)]
        public void First(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(2)]
        public void Second(Guid bar)
        {
            WriteEvent(2, bar);
        }

        [Event(3)]
        public void Third(Guid relatedActivityId)
        {
            WriteEventWithRelatedActivityId(3, relatedActivityId);
        }

        [Event(4, Opcode = EventOpcode.Send)]
        public void Forth(Guid relatedActivityId)
        {
            WriteEventWithRelatedActivityId(4, relatedActivityId);
        }

        [Event(5, Opcode = EventOpcode.Receive)]
        public void Fifth(Guid relatedActivityId)
        {
            WriteEventWithRelatedActivityId(5, relatedActivityId);
        }

        [Event(6)]
        public void Sixth()
        {
            WriteEvent(6);
        }
    }
}