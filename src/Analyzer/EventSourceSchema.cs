using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer
{
    /// <summary>
    /// Represents a schema for an <see cref="EventSource"/>.
    /// </summary>
    public class EventSourceSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourceSchema"/> class.
        /// </summary>
        /// <param name="providerId">An identifier of the event provider.</param>
        /// <param name="providerName">A name of the event provider.</param>
        public EventSourceSchema(Guid providerId, string providerName)
        {
            if (providerId == Guid.Empty)
            {
                throw new ArgumentException(ExceptionMessages.ProviderIdMayNotBeEmpty,
                    nameof(providerId));
            }
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            ProviderId = providerId;
            ProviderName = providerName;
        }

        /// <summary>
        /// Gets the identifier of the event provider.
        /// </summary>
        public Guid ProviderId { get; }

        /// <summary>
        /// Gets the name of the event provider.
        /// </summary>
        public string ProviderName { get; }

        /// <summary>
        /// Gets the event schemas of the event provider.
        /// </summary>
        public IReadOnlyCollection<EventSchema> Events { get; internal set; }
    }
}