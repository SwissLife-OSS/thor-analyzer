using System.Collections.Generic;
using System.Collections.Immutable;

namespace ChilliCream.Tracing.Schema
{
    public class EventSourceSchemaError
    {
        public EventSourceSchemaError(EventSourceSchemaErrorCodes errorCode, IEnumerable<EventSchema> eventSchema)
        {
            Code = errorCode;
            Events = eventSchema.ToImmutableArray();
        }

        public EventSourceSchemaErrorCodes Code { get; }
        public IReadOnlyList<EventSchema> Events { get; }
    }
}