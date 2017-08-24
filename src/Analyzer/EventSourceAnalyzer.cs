using ChilliCream.Tracing.Analyzer.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;

namespace ChilliCream.Tracing.Analyzer
{
    public class EventSourceAnalyzer
    {
        private readonly SchemaCache _cache = new SchemaCache();
        private List<IRuleSet> _ruleSets = new List<IRuleSet>();

        public EventSourceAnalyzer()
            : this(new IRuleSet[] { new RequiredRuleSet(), new BestPracticeRuleSet() })
        { }

        public EventSourceAnalyzer(IEnumerable<IRuleSet> ruleSets)
        {
            AddRange(ruleSets);
        }

        public IReadOnlyCollection<IRuleSet> RuleSets { get { return _ruleSets; } }

        public void Add(IRuleSet ruleSet)
        {
            _ruleSets.Add(ruleSet);
        }

        public void AddRange(IEnumerable<IRuleSet> ruleSets)
        {
            _ruleSets.AddRange(ruleSets);
        }

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