using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Schema
{
    /// <summary>
    /// Represents an <see cref="EventSource"/> schema.
    /// </summary>
    public sealed class EventSchema
    {
        private EventSourceSchema _eventSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Schema.EventSchema"/> class with the specified values.
        /// </summary>
        /// <param name="id">The event id.</param>
        /// <param name="id">The event name.</param>
        /// <param name="providerId">The provider GUID.</param>
        /// <param name="providerName">The provider name.</param>
        /// <param name="level">The event level.</param>
        /// <param name="task">The event task.</param>
        /// <param name="taskName">The event task name.</param>
        /// <param name="opcode">The event operation code.</param>
        /// <param name="opcodeName">The event operation code name.</param>
        /// <param name="keywords">The event keywords.</param>
        /// <param name="keywordsDescription">The event keywords description.</param>
        /// <param name="version">The event version.</param>
        /// <param name="payload">The event payload.</param>
        public EventSchema(int id, string name, EventLevel level,
            EventTask task, string taskName, EventOpcode opcode, string opcodeName,
            EventKeywords keywords, string keywordsDescription, int version,
            IEnumerable<string> payload)
        {
            Id = id;
            Name = name;
            Level = level;
            Task = task;
            TaskName = taskName;
            Opcode = opcode;
            OpcodeName = opcodeName;
            Keywords = keywords;
            KeywordsDescription = keywordsDescription;
            Version = version;
            Payload = payload.ToImmutableArray();
        }

        /// <summary>
        /// Gets the event ID.
        /// </summary>
        /// <value>The event ID.</value>
        public int Id { get; }

        /// <summary>
        /// Gets the event task.
        /// </summary>
        /// <remarks>
        /// Events for a given provider can be given a group identifier called a Task that indicates the
        /// broad area within the provider that the event pertains to (for example the Kernel provider has
        /// Tasks for Process, Threads, etc). 
        /// </remarks>
        /// <value>The event task.</value>
        public EventTask Task { get; }

        /// <summary>
        /// Gets the task name.
        /// </summary>
        /// <value>The task name.</value>
        public string TaskName { get; }

        /// <summary>
        /// Gets the payload names that maps to the event signature parameter names.
        /// </summary>
        /// <value>The event payload.</value>
        public ImmutableArray<string> Payload { get; }

        /// <summary>
        /// Gets the operation code.
        /// </summary>
        /// <remarks>
        /// Each event has a Type identifier that indicates what kind of an event is being logged. Note that
        /// providers are free to extend this set, so the id may not be just the value in <see cref="Opcode"/>.
        /// </remarks>
        /// <value>The operation code.</value>
        public EventOpcode Opcode { get; }

        /// <summary>
        /// Gets the human-readable string name for the <see cref="EventSchema.Opcode"/> property. 
        /// </summary>
        /// <value>The operation code name.</value>
        public string OpcodeName { get; }

        /// <summary>
        /// Gets the event level.
        /// </summary>
        /// <value>The event level.</value>
        public EventLevel Level { get; }

        /// <summary>
        /// Gets the event version.
        /// </summary>
        /// <value>The event version.</value>
        public int Version { get; }

        /// <summary>
        /// Gets the event keywords.
        /// </summary>
        /// <value>The event keywords.</value>
        public EventKeywords Keywords { get; }

        /// <summary>
        /// Gets the human-readable string name for the <see cref="EventSchema.Keywords"/> property. 
        /// </summary>
        /// <value>The keyword description.</value>
        public string KeywordsDescription { get; }

        /// <summary>
        /// Gets the name for the event.
        /// </summary>
        /// <remarks>
        /// This is simply the concatenation of the task and operation code names.
        /// </remarks>
        /// <value>The event name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the event source schema.
        /// </summary>
        /// <value>The event source schema.</value>
        public EventSourceSchema EventSource
        {
            get
            {
                return _eventSource;
            }
            internal set
            {
                if (_eventSource != null)
                {
                    throw new InvalidOperationException("The event source schema can be set only once.");
                }
                _eventSource = value;
            }
        }
    }
}