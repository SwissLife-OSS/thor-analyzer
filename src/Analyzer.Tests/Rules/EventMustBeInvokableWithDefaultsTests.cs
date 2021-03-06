﻿using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using System.Linq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class EventMustBeInvokableWithDefaultsTests
        : EventRuleTestBase<EventMustBeInvokableWithDefaults>
    {
        protected override EventMustBeInvokableWithDefaults CreateRule(IRuleSet ruleSet)
        {
            return new EventMustBeInvokableWithDefaults(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if events were not invokable with defaults")]
        public void Apply_Error()
        {
            // arrange
            EventNotWorkingWithDefaultsEventSource eventSource = 
                EventNotWorkingWithDefaultsEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema.Events.First(), eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
            ((Error)result).Details.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Apply: Should return a success if events were invokable with defaults")]
        public void Apply_Success()
        {
            // arrange
            EventWorkingWithDefaultsEventSource eventSource =
                EventWorkingWithDefaultsEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema.Events.First(), eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Success>();
        }
    }
}