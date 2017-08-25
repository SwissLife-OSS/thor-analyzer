using ChilliCream.Tracing.Analyzer.Rules;
using ChilliCream.Tracing.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class SuccessTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for ruleSets")]
        public void Constructor_RuleSetsNull()
        {
            // arrange
            IRuleSet[] ruleSets = null;

            // act
            Action throwException = () => new EventSourceAnalyzer(ruleSets);

            // assert
            throwException.ShouldThrowNull("ruleSets");
        }

        [Fact(DisplayName = "Add: Should throw an argument null exception for ruleSet")]
        public void Add_RuleSetNull()
        {
            // arrange
            IRuleSet ruleSet = null;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            Action throwException = () => analyzer.Add(ruleSet);

            // assert
            throwException.ShouldThrowNull("ruleSet");
        }

        [Fact(DisplayName = "Add: Should add a single ruleSet")]
        public void Add_Success()
        {
            // arrange
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer(new IRuleSet[0]);

            // act
            analyzer.Add(ruleSet);

            // assert
            analyzer.RuleSets.Should().HaveCount(1);
        }

        [Fact(DisplayName = "AddRange: Should throw an argument null exception for ruleSets")]
        public void AddRange_RuleSetsNull()
        {
            // arrange
            IRuleSet[] ruleSets = null;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            Action throwException = () => analyzer.AddRange(ruleSets);

            // assert
            throwException.ShouldThrowNull("ruleSets");
        }

        [Fact(DisplayName = "AddRange: Should add multiple ruleSets")]
        public void AddRange_Success()
        {
            // arrange
            IRuleSet[] ruleSets = new IRuleSet[]
            {
                new Mock<IRuleSet>().Object,
                new Mock<IRuleSet>().Object
            };
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer(new IRuleSet[0]);

            // act
            analyzer.AddRange(ruleSets);

            // assert
            analyzer.RuleSets.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Inspect: Should throw an argument null exception for eventSource")]
        public void Inspect_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            Action throwException = () => analyzer.Inspect(eventSource);

            // assert
            throwException.ShouldThrowNull("eventSource");
        }

        [Fact(DisplayName = "Inspect: Should return only success status")]
        public void Inspect_Success()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            Report report = analyzer.Inspect(eventSource);

            // assert
            report.HasErrors.Should().BeFalse();

            //int eventCount = new SchemaReader(eventSource).Read().Events.Count();
            //int eventSourceRuleCount = analyzer.RuleSets.SelectMany(r => r.Rules.OfType<IEventSourceRule>()).Count();
            //int eventRuleCount = analyzer.RuleSets.SelectMany(r => r.Rules.OfType<IEventRule>()).Count() * eventCount;

            //report.Should().HaveCount(eventSourceRuleCount + eventRuleCount);
            //report.All(r => r.GetType().IsAssignableFrom(typeof(Success))).Should().BeTrue();
        }
    }
}