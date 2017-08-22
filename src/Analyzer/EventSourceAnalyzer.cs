using ChilliCream.Tracing.Analyzer.Rules;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Tracing;
using System.Linq;

namespace ChilliCream.Tracing.Analyzer
{
    public class EventSourceAnalyzer
    {
        private readonly SchemaCache _cache = new SchemaCache();
        private ImmutableArray<IRuleSet> _ruleSets = ImmutableArray<IRuleSet>.Empty;

        public EventSourceAnalyzer()
            : this(new IRuleSet[] { new RequiredRuleSet(), new BestPracticeRuleSet() })
        { }

        public EventSourceAnalyzer(IEnumerable<IRuleSet> ruleSets)
        {
            AddRange(ruleSets);
        }

        public IEnumerable<IRuleSet> RuleSets { get { return _ruleSets; } }

        public void Add(IRuleSet ruleSet)
        {
            _ruleSets = _ruleSets.Add(ruleSet);
        }

        public void AddRange(IEnumerable<IRuleSet> ruleSets)
        {
            _ruleSets = _ruleSets.AddRange(ruleSets);
        }

        public IEnumerable<IResult> Inspect(EventSource eventSource)
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

            ImmutableArray<IResult> results = ImmutableArray<IResult>.Empty;

            foreach (IRuleSet ruleSet in _ruleSets)
            {
                foreach (IEventSourceRule rule in ruleSet.Rules.OfType<IEventSourceRule>())
                {
                    IResult result = rule.Apply(schema, eventSource);

                    results = results.Add(result);
                }

                IEnumerable<IEventRule> eventRules = ruleSet.Rules.OfType<IEventRule>();

                foreach (EventSchema eventSchema in schema.Events)
                {
                    foreach (IEventRule rule in eventRules)
                    {
                        IResult result = rule.Apply(eventSchema, eventSource);

                        results = results.Add(result);
                    }
                }
            }

            return results;
        }
    }
}