using ChilliCream.Logging.Analyzer.Rules;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Tracing;
using System.Linq;

namespace ChilliCream.Logging.Analyzer
{
    public class EventSourceAnalyzer
    {
        private readonly List<IRuleSet> _ruleSets = new List<IRuleSet>();
        private readonly SchemaCache _cache = new SchemaCache();

        public EventSourceAnalyzer()
        {
            _ruleSets.Add(new BasicRuleSet());
            _ruleSets.Add(new AdvancedRuleSet());
        }

        public EventSourceAnalyzer(IEnumerable<IRuleSet> ruleSets)
        {
            _ruleSets.AddRange(ruleSets);
        }

        public void Append(IRuleSet ruleSet)
        {
            _ruleSets.Add(ruleSet);
        }

        public IEnumerable<IResult> Inspect(EventSource eventSource)
        {
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