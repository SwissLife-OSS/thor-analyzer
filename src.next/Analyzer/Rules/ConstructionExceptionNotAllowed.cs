using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for duplicate event identifiers.
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
                return new Error(this, "An exception during creation of the event source occurred. " +
                    "See details for exception message.", new [] { eventSource.ConstructionException.Message });
            }

            return new Success(this);
        }
    }
}