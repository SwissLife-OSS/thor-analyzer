using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class MustHaveStaticLogFieldOrPropertyTests
        : EventSourceRuleTestBase<MustHaveStaticLogFieldOrProperty>
    {
        protected override MustHaveStaticLogFieldOrProperty CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveStaticLogFieldOrProperty(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if log field and property does not exist")]
        public void Apply_NoLogFieldAndProperty()
        {
            // arrange
            LogFieldAndPropertyDoNotExistEventSource eventSource = new LogFieldAndPropertyDoNotExistEventSource();
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
        public void Apply_LogFieldNull()
        {
            // arrange
            LogFieldNullEventSource eventSource = new LogFieldNullEventSource();
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

        [Fact(DisplayName = "Apply: Should return a success if the log field was found as expected")]
        public void Apply_LogFieldAsExpected()
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

        [Fact(DisplayName = "Apply: Should return an error if log property is not public")]
        public void Apply_LogPropertyNotPublic()
        {
            // arrange
            LogPropertyNotPublicEventSource eventSource = new LogPropertyNotPublicEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if log property is not readonly")]
        public void Apply_LogPropertyNotReadOnly()
        {
            // arrange
            LogPropertyNotReadOnlyEventSource eventSource = new LogPropertyNotReadOnlyEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if log property is not static")]
        public void Apply_LogPropertyNotStatic()
        {
            // arrange
            LogPropertyNotStaticEventSource eventSource = new LogPropertyNotStaticEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if log property returns no value")]
        public void Apply_LogPropertyNull()
        {
            // arrange
            LogPropertyNullEventSource eventSource = new LogPropertyNullEventSource();
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

        [Fact(DisplayName = "Apply: Should return a success if the log property was found as expected")]
        public void Apply_LogPropertyAsExpected()
        {
            // arrange
            LogPropertyEventSource eventSource = new LogPropertyEventSource();
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
 