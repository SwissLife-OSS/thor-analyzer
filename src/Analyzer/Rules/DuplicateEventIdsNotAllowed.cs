using System;
using System.Collections.Generic;
using System.Linq;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A rule which probes for duplicate event identifiers.
    /// </summary>
    public class DuplicateEventIdsNotAllowed
        : IEventSourceRule
    {
        private readonly Dictionary<int, int> _duplicateIds = new Dictionary<int, int>();

        /// <summary>
        /// Initiates a new instance of the <see cref="DuplicateEventIdsNotAllowed"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public DuplicateEventIdsNotAllowed(IRuleSet ruleSet)
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

            foreach (IGrouping<int, EventSchema> group in schema.Events.GroupBy(s => s.Id))
            {
                int count = group.Count();

                if (count > 1)
                {
                    _duplicateIds.Add(group.Key, count);
                }
            }

            if (_duplicateIds.Count > 0)
            {
                List<string> details = new List<string>();

                foreach(KeyValuePair<int, int> duplicateId in _duplicateIds)
                {
                    details.Add($"The event identifier {duplicateId.Key} was found " +
                        "{duplicateId.Value} times.");
                }

                return new Error(this, $"{_duplicateIds.Count} duplicate identifier(s) found. " +
                    "See details for more information.", details);
            }

            return new Success(this);
        }
    }
}