using System;
using System.Collections.Generic;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for construction exceptions.
    /// </summary>
    public class ConstructionExceptionNotAllowed
        : IEventSourceRule
    {
        private readonly Dictionary<int, int> _duplicateIds = new Dictionary<int, int>();

        /// <summary>
        /// Initiates a new instance of the <see cref="ConstructionExceptionNotAllowed"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public ConstructionExceptionNotAllowed(IRuleSet ruleSet)
        {
            if (ruleSet == null)
            {
                throw new ArgumentNullException(nameof(ruleSet));
            }

            RuleSet = ruleSet;
        }

        /// <inheritdoc/>
        public IRuleSet RuleSet { get; }

        /// <inheritdoc/>
        public IResult Apply(EventSourceSchema schema, EventSource eventSource)
        {
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            if (eventSource.ConstructionException != null)
            {
                string[] details = new[] { eventSource.ConstructionException.Message };

                return new Error(this, "An exception during creation of the event source occurred. " +
                    "See details for exception message.", details);
            }

            return new Success(this);
        }
    }
}