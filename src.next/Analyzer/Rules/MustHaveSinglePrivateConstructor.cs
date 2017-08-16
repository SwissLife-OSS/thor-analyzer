using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for a single private constructor.
    /// </summary>
    public class MustHaveSinglePrivateConstructor
        : IEventSourceRule
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveSinglePrivateConstructor"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveSinglePrivateConstructor(IRuleSet ruleSet)
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

            Type eventSourceType = eventSource.GetType();
            IEnumerable<ConstructorInfo> constructors = eventSourceType.GetConstructors()
                .Concat(eventSourceType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic))
                .Concat(eventSourceType.GetConstructors(BindingFlags.Instance | BindingFlags.Static));

            if (constructors.Count() != 1 || !constructors.First().IsPrivate || constructors.First().IsStatic ||
                constructors.First().GetParameters().Length > 0)
            {
                return new Error(this, "Did not found a single private constructor without any parameter.");
            }

            return new Success(this);
        }
    }
}