using System;
using System.Reflection;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for missing <c>Log</c> properties.
    /// </summary>
    public class MustHaveStaticLogProperty
        : IEventSourceRule
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveStaticLogProperty"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveStaticLogProperty(IRuleSet ruleSet)
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
            FieldInfo field = eventSourceType.GetField("Log");

            if (field == null || !field.IsStatic || !field.IsInitOnly ||
                !field.FieldType.IsAssignableFrom(eventSourceType) || field.GetValue(null) == null)
            {
                return new Error(this, "Did not found a public readonly 'Log' field which is " +
                    "static and holds an instance of its own type.");
            }

            return new Success(this);
        }
    }
}