using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer
{
    /// <summary>
    /// An event listener to probe events.
    /// </summary>
    public class ProbeEventListener
        : EventListener
    {
        private readonly ConcurrentQueue<EventWrittenEventArgs> _queue = new ConcurrentQueue<EventWrittenEventArgs>();

        /// <summary>
        /// A collection of ordered events which has been recorded during a session.
        /// </summary>
        public IEnumerable<EventWrittenEventArgs> OrderedEvents => _queue.ToArray();

        /// <inheritdoc/>
        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            _queue.Enqueue(eventData);
        }
    }
}