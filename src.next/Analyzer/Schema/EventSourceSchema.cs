using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace ChilliCream.Tracing.Schema
{
    public class EventSourceSchema
    {
        internal EventSourceSchema(Guid id, string name, IEnumerable<EventSchema> events)
        {
            Id = id;
            Name = name;
            Events = events.ToImmutableDictionary(t => t.Id);

            foreach (EventSchema eventSchema in Events.Values)
            {
                eventSchema.EventSource = this;
            }
        }

        public Guid Id { get; }
        public string Name { get; }
        public IReadOnlyDictionary<int, EventSchema> Events { get; }
    }
}