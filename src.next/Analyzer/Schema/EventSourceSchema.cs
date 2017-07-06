using System;
using System.Collections.Generic;

namespace ChilliCream.Tracing.Schema
{
    public class EventSourceSchema
    {
        internal EventSourceSchema(Guid guid, string name,
            IReadOnlyDictionary<int, EventSchema> events,
            IReadOnlyCollection<EventSourceSchemaError> errors)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (events == null)
            {
                throw new ArgumentNullException(nameof(events));
            }
            if (errors == null)
            {
                throw new ArgumentNullException(nameof(errors));
            }

            Guid = guid;
            Name = name;
            Events = events;
            Errors = errors;
        }

        public Guid Guid { get; }
        public string Name { get; }
        public IReadOnlyDictionary<int, EventSchema> Events { get; }
        public IReadOnlyCollection<EventSourceSchemaError> Errors { get; }
    }
}