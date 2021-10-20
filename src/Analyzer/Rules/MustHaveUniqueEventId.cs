using System;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer.Rules
{
    /// <summary>
    /// A rule which verifies if the event source has unique event identifiers.
    /// </summary>
    public class MustHaveUniqueEventId
        : IEventSourceRule
    {
        private static Regex _namePattern = new Regex(@"^[a-zA-Z]+(\-[a-zA-Z]+)*$",
            RegexOptions.Compiled | RegexOptions.Singleline);

        /// <summary>
        /// Initiates a new instance of the <see cref="MustHaveUniqueEventId"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public MustHaveUniqueEventId(IRuleSet ruleSet)
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
            var attributeMap = eventSource
                .GetMethods()
                .Select(mi => new
                {
                    MethodInfo = mi,
                    EventAttribute = mi.GetEvent()
                })
                .Where(mi => mi.EventAttribute != null);

            var duplicates = attributeMap
                .GroupBy(map => map.EventAttribute.EventId)
                .Where(g => g.Count() > 1)
                .ToDictionary(
                    k => k.Key,
                    v => v.Select(s => s.MethodInfo.Name).ToArray());

            if(duplicates.Any())
            {
                string duplicatesString = string.Join(
                    " / ",
                    duplicates.Select(dup => $"[{dup.Key}] -> [{string.Join(", ", dup.Value)}]"));
                
                return new Error(
                    this,
                    "The EventSource must have unique event identifiers. " +
                    $"Duplicates: {duplicatesString}");
            }

            if (!_namePattern.IsMatch(schema.ProviderName))
            {
                return new Error(this, "The EventSource must have a valid name.");
            }

            return new Success(this);
        }
    }
}