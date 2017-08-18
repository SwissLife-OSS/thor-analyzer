using ChilliCream.Logging.Analyzer.Rules;
using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests
{
    public class EventSourceAnalyzerTests
    {
        [Fact(DisplayName = "Inspect: Should throw an argument null exception for eventSource")]
        public void Inspect_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            Action validate = () => analyzer.Inspect(eventSource);

            // assert
            validate.ShouldThrowArgumentNull("eventSource");
        }

        [Fact(DisplayName = "Inspect: Should return only success status")]
        public void Inspect_Success()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            IEnumerable<IResult> results = analyzer.Inspect(eventSource);

            // assert
            int eventCount = new SchemaReader(eventSource).Read().Events.Count();
            int eventSourceRuleCount = analyzer.RuleSets.SelectMany(r => r.Rules.OfType<IEventSourceRule>()).Count();
            int eventRuleCount = analyzer.RuleSets.SelectMany(r => r.Rules.OfType<IEventRule>()).Count() * eventCount;

            results.Should().HaveCount(eventSourceRuleCount + eventRuleCount);
            results.All(r => r.GetType().IsAssignableFrom(typeof(Success))).Should().BeTrue();
        }
    }
}