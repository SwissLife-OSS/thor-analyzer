// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema
{
    /// <summary>
    /// Used for caching <see cref="EventSchema"/>.
    /// </summary>
    public class EventSourceSchemaCache
    {
        private readonly ConcurrentDictionary<Guid, IImmutableDictionary<int, EventSchema>> _schemas = new ConcurrentDictionary<Guid, IImmutableDictionary<int, EventSchema>>();

        static EventSourceSchemaCache()
        {
            Instance = new EventSourceSchemaCache();
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="EventSourceSchemaCache"/>.
        /// </summary>
        /// <value>The instance of EventSourceSchemaCache.</value>
        public static EventSourceSchemaCache Instance { get; private set; }

        /// <summary>
        /// Gets the <see cref="EventSchema"/> for the specified eventId and eventSource.
        /// </summary>
        /// <param name="eventId">The ID of the event.</param>
        /// <param name="eventSource">The event source.</param>
        /// <returns>The EventSchema.</returns>
        public EventSchema GetSchema(int eventId, EventSource eventSource)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            IImmutableDictionary<int, EventSchema> events;

            if (!_schemas.TryGetValue(eventSource.Guid, out events))
            {
                events = EventSourceSchemaReader.GetSchema(eventSource);
                _schemas[eventSource.Guid] = events;
            }

            return events[eventId];
        }
    }
}
