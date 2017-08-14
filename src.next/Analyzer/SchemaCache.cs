using System;
using System.Collections.Concurrent;

namespace ChilliCream.Logging.Analyzer
{
    /// <summary>
    /// A bunch of convenient <see cref="EventSource/> extension methods.
    /// </summary>
    public class SchemaCache
    {
        private readonly ConcurrentDictionary<Guid, EventSourceSchema> _schemas = new ConcurrentDictionary<Guid, EventSourceSchema>();

        /// <summary>
        /// Tries to add a schema to the cache.
        /// </summary>
        /// <param name="schema">A schema that should be cached.</param>
        /// <returns><c>true</c> if successfully added; otherwise <c>false</c> if already exists in the cache.</returns>
        public bool TryAdd(EventSourceSchema schema)
        {
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            return _schemas.TryAdd(schema.ProviderId, schema);
        }

        /// <summary>
        /// Tries to get a schema from the cache.
        /// </summary>
        /// <param name="providerId">A provider identifier.</param>
        /// <param name="schema">A schema for the specified provider identifier.</param>
        /// <returns><c>true</c> if a schema was found; otherwise <c>false</c>.</returns>
        public bool TryGet(Guid providerId, out EventSourceSchema schema)
        {
            if (providerId == Guid.Empty)
            {
                throw new ArgumentException(ExceptionMessages.ProviderIdMayNotBeEmpty, nameof(providerId));
            }

            return _schemas.TryGetValue(providerId, out schema);
        }

        /// <summary>
        /// Clears the complete schema cache.
        /// </summary>
        public void Clear()
        {
            _schemas.Clear();
        }
    }
}