using ChilliCream.Tracing.Analyzer.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace ChilliCream.Tracing.Analyzer
{
    /// <summary>
    /// A event provider (<see cref="EventSource"/>) analyzer that generates a report.
    /// </summary>
    public class EventSourceAnalyzer
    {
        private readonly SchemaCache _cache = new SchemaCache();
        private List<IRuleSet> _ruleSets = new List<IRuleSet>();

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourceAnalyzer"/> class.
        /// </summary>
        public EventSourceAnalyzer()
            : this(new IRuleSet[] { new RequiredRuleSet(), new BestPracticeRuleSet() })
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourceAnalyzer"/> class.
        /// </summary>
        /// <param name="ruleSets">A rule set.</param>
        /// <remarks>Use this ctor to override the complete set of rule sets.</remarks>
        public EventSourceAnalyzer(IEnumerable<IRuleSet> ruleSets)
        {
            AddRange(ruleSets);
        }

        /// <summary>
        /// Gets all rule sets.
        /// </summary>
        public IReadOnlyCollection<IRuleSet> RuleSets { get { return _ruleSets; } }

        /// <summary>
        /// Adds on rule set.
        /// </summary>
        /// <param name="ruleSet">A rule set.</param>
        public void Add(IRuleSet ruleSet)
        {
            _ruleSets.Add(ruleSet);
        }

        /// <summary>
        /// Adds a collection of rule sets.
        /// </summary>
        /// <param name="ruleSets">A collection of rule sets.</param>
        public void AddRange(IEnumerable<IRuleSet> ruleSets)
        {
            _ruleSets.AddRange(ruleSets);
        }

        /// <summary>
        /// Analyzes the event provider against the defined rule sets and generates a report subsequently.
        /// </summary>
        /// <param name="eventSource">An event provider.</param>
        /// <returns>A report.</returns>
        public IReadOnlyCollection<IResult> Inspect(EventSource eventSource)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            EventSourceSchema schema;

            if (!_cache.TryGet(eventSource.Guid, out schema))
            {
                SchemaReader reader = new SchemaReader(eventSource);

                schema = reader.Read();
                _cache.TryAdd(schema);
            }

            List<IResult> results = new List<IResult>();

            foreach (IRuleSet ruleSet in _ruleSets)
            {
                foreach (IEventSourceRule rule in ruleSet.Rules.OfType<IEventSourceRule>())
                {
                    IResult result = rule.Apply(schema, eventSource);

                    results.Add(result);
                }

                IEnumerable<IEventRule> eventRules = ruleSet.Rules.OfType<IEventRule>();

                foreach (EventSchema eventSchema in schema.Events)
                {
                    foreach (IEventRule rule in eventRules)
                    {
                        IResult result = rule.Apply(eventSchema, eventSource);

                        results.Add(result);
                    }
                }
            }

            return results;
        }
    }
}