using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer
{
    public class ProbeEventListener
        : EventListener
    {
        private readonly ConcurrentQueue<EventWrittenEventArgs> _queue = new ConcurrentQueue<EventWrittenEventArgs>();

        public IEnumerable<EventWrittenEventArgs> OrderdEvents => _queue.ToArray();

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            _queue.Enqueue(eventData);
        }
    }
}