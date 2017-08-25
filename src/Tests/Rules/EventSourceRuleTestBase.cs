using ChilliCream.Tracing.Analyzer.Rules;
using FluentAssertions;
using Moq;
using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests.Rules
{
    public abstract class EventSourceRuleTestBase<TRule>
        where TRule : IEventSourceRule
    {
        protected abstract TRule CreateRule(IRuleSet ruleSet);

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for ruleSet")]
        public void Constructor_RuleSetNull()
        {
            // arrange
            IRuleSet ruleSet = null;

            // act
            Action throwException = () => CreateRule(ruleSet);

            // assert
            throwException.ShouldThrowNull("ruleSet");
        }

        [Fact(DisplayName = "Constructor: Should not throw any exception")]
        public void Constructor_Success()
        {
            // arrange
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;

            // act
            IRule rule = CreateRule(ruleSet);

            // assert
            rule.Should().NotBeNull();
            rule.RuleSet.Should().Be(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should throw an argument null exception for schema")]
        public void Apply_SchemaNull()
        {
            // arrange
            EventSource eventSource = null;
            EventSourceSchema schema = null;
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            TRule rule = CreateRule(ruleSet);

            // act
            Action throwException = () => rule.Apply(schema, eventSource);

            // assert
            throwException.ShouldThrowNull("schema");
        }

        [Fact(DisplayName = "Apply: Should throw an argument null exception for eventSource")]
        public void Apply_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;
            EventSourceSchema schema = new EventSourceSchema(Guid.NewGuid(), "ProviderName");
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            TRule rule = CreateRule(ruleSet);

            // act
            Action throwException = () => rule.Apply(schema, eventSource);

            // assert
            throwException.ShouldThrowNull("eventSource");
        }
    }
}