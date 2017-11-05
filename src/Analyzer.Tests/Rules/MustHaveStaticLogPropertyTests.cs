using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class MustHaveStaticLogPropertyTests
        : EventSourceRuleTestBase<MustHaveStaticLogProperty>
    {
        protected override MustHaveStaticLogProperty CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveStaticLogProperty(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if log field does not exist")]
        public void Apply_NoLogField()
        {
            // arrange
            LogFieldDoesNotExistEventSource eventSource = new LogFieldDoesNotExistEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
        }

        [Fact(DisplayName = "Apply: Should return an error if log field is not public")]
        public void Apply_LogFieldNotPublic()
        {
            // arrange
            LogFieldNotPublicEventSource eventSource = new LogFieldNotPublicEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
        }

        [Fact(DisplayName = "Apply: Should return an error if log field is not readonly")]
        public void Apply_LogFieldNotReadOnly()
        {
            // arrange
            LogFieldNotReadOnlyEventSource eventSource = new LogFieldNotReadOnlyEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
        }

        /*
        todo: figure out why this test leads to StackOverflowException

        [Fact(DisplayName = "Apply: Should return an error if log field is not static")]
        public void Apply_LogFieldNotStatic()
        {
            // arrange
            LogFieldNotStaticEventSource eventSource = new LogFieldNotStaticEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
        }
        */

        [Fact(DisplayName = "Apply: Should return an error if log field returns no value")]
        public void Apply_LogFieldNoValue()
        {
            // arrange
            LogFieldDoesNotHaveValueEventSource eventSource =
                new LogFieldDoesNotHaveValueEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
        }

        [Fact(DisplayName = "Apply: Should return a success if a log field as defined was found")]
        public void Apply_Success()
        {
            // arrange
            LogFieldEventSource eventSource = new LogFieldEventSource();
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Success>();
        }
    }
}