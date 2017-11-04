using System.Collections.Generic;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A rule set which contains rules for the vitality of the <see cref="EventSource"/>.
    /// </summary>
    public class RequiredRuleSet
        : IRuleSet
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="RequiredRuleSet"/> class.
        /// </summary>
        public RequiredRuleSet()
        {
            Rules = new IRule[]
            {
                new ConstructionExceptionNotAllowed(this),
                new DuplicateEventIdsNotAllowed(this),
                new EventMustBeInvokable(this),
                new EventMustBeInvokableWithDefaults(this),
                new EventParametersMustBeInOrder(this)
            };
        }

        /// <inheritdoc/>
        public IEnumerable<IRule> Rules { get; }
    }
}