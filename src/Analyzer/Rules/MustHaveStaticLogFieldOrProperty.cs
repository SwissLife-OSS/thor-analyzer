using System;
using System.Reflection;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for missing <c>Log</c> field or property.
    /// </summary>
    public class MustHaveStaticLogFieldOrProperty
        : IEventSourceRule
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveStaticLogFieldOrProperty"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveStaticLogFieldOrProperty(IRuleSet ruleSet)
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
            PropertyInfo property = eventSourceType.GetProperty("Log");

            if (property == null)
            {
                FieldInfo field = eventSourceType.GetField("Log");

                if (field == null || !field.IsStatic || !field.IsInitOnly ||
                    !field.FieldType.IsAssignableFrom(eventSourceType) ||
                    field.GetValue(null) == null)
                {
                    return CreateError();
                }
            }
            else if (!property.CanRead || property.CanWrite || !property.GetGetMethod().IsStatic ||
                !property.PropertyType.IsAssignableFrom(eventSourceType) ||
                property.GetValue(null) == null)
            {
                return CreateError();
            }

            return new Success(this);
        }

        private Error CreateError()
        {
            return new Error(this, "Did not found a public readonly 'Log' field or property " +
                "which is static and holds an instance of its own type.");
        }
    }
}