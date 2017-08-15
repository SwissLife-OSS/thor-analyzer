using ChilliCream.Logging.Analyzer.Rules;
using Moq;
using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests.Rules
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
            Action validate = () => CreateRule(ruleSet);

            // assert
            validate.ShouldThrowArgumentNull("ruleSet");
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
            Action validate = () => rule.Apply(schema, eventSource);

            // assert
            validate.ShouldThrowArgumentNull("schema");
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
            Action validate = () => rule.Apply(schema, eventSource);

            // assert
            validate.ShouldThrowArgumentNull("eventSource");
        }
    }
}