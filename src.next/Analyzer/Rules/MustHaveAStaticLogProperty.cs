using System;
using System.Diagnostics.Tracing;
using System.Reflection;

namespace ChilliCream.Logging.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for missing <c>Log</c> properties.
    /// </summary>
    public sealed class MustHaveAStaticLogProperty
        : IEventSourceRule
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveAStaticLogProperty"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveAStaticLogProperty(IRuleSet ruleSet)
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

            if (field == null)
            {
                return new Error(this, "Public, static field that returns an instance of the specific EventSource implementation is missing.");
            }

            if (field.FieldType != eventSourceType)
            {
                return new Error(this, "The Log field must return an instance of its own type.");
            }

            if (!field.IsStatic)
            {
                return new Error(this, "The Log field must be static.");
            }

            return new Success(this);
        }
    }
}