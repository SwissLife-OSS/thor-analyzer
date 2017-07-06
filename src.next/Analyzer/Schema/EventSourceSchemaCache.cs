using System;
using System.Collections.Concurrent;
using Microsoft.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Schema
{
    /// <summary>
    /// Used for caching <see cref="EventSchema"/>.
    /// </summary>
    public static class EventSourceSchemaCache
    {
        private static readonly ConcurrentDictionary<Guid, EventSourceSchema>
            _schemas = new ConcurrentDictionary<Guid, EventSourceSchema>();

        /// <summary>
        /// Gets the <see cref="EventSchema"/> for the specified eventId and eventSource.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <param name="eventSource">The event source.</param>
        /// <returns>The EventSchema.</returns>
        public static EventSchema GetEventSchema(this EventSource eventSource, int eventId)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            EventSourceSchema schema;

            if (!_schemas.TryGetValue(eventSource.Guid, out schema))
            {
                schema = eventSource.GetSchema();
                _schemas[eventSource.Guid] = schema;
            }

            return schema.Events[eventId];
        }

        /// <summary>
        /// Tries to gets the <see cref="EventSchema"/> for the specified eventId and eventSource.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <param name="eventSource">The event source.</param>
        /// <param name="eventSchema">The <see cref="EventSchema"/> that is associated with the specified <paramref name="eventId"/>.</param>
        /// <returns>Returns <c>true</c> if there is an event with the specified <paramref name="eventId"/>; otherwise <c>false</c>.</returns>
        public static bool TryGetEventSchema(this EventSource eventSource, int eventId, out EventSchema eventSchema)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            EventSourceSchema schema;

            if (!_schemas.TryGetValue(eventSource.Guid, out schema))
            {
                schema = eventSource.GetSchema();
                _schemas[eventSource.Guid] = schema;
            }

            return schema.Events.TryGetValue(eventId, out eventSchema);
        }

        /// <summary>
        /// Clears the event source schema cache.
        /// </summary>
        public static void Clear()
        {
            _schemas.Clear();
        }
    }
}