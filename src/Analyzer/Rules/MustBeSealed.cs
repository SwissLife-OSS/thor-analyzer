using System;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// A rule which verifies if the event source is sealed.
    /// </summary>
    public class MustBeSealed
        : IEventSourceRule
    {

        /// <summary>
        /// Initiates a new instance of the <see cref="MustBeSealed"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustBeSealed(IRuleSet ruleSet)
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

            if (!eventSource.GetType().IsSealed)
            {
                return new Error(this, "The EventSource must be sealed.");
            }

            return new Success(this);
        }
    }
}