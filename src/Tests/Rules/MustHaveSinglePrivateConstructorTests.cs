using ChilliCream.Tracing.Analyzer.Rules;
using ChilliCream.Tracing.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests.Rules
{
    public class MustHaveSinglePrivateConstructorTests
        : EventSourceRuleTestBase<MustHaveSinglePrivateConstructor>
    {
        protected override MustHaveSinglePrivateConstructor CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveSinglePrivateConstructor(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if constructor does not exist")]
        public void Apply_NoConstructor()
        {
            // arrange
            ConstructorDoesNotExistEventSource eventSource = new ConstructorDoesNotExistEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if the constructor is not private")]
        public void Apply_ConstructorNotPrivate()
        {
            // arrange
            ConstructorNotPrivateEventSource eventSource = new ConstructorNotPrivateEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if the constructor is static")]
        public void Apply_ConstructorStatic()
        {
            // arrange
            ConstructorStaticEventSource eventSource = new ConstructorStaticEventSource();
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

        [Fact(DisplayName = "Apply: Should return an error if there are multiple constructors")]
        public void Apply_MultipleConstructors()
        {
            // arrange
            MultipleConstructorsEventSource eventSource = MultipleConstructorsEventSource.Log;
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

        [Fact(DisplayName = "Apply: Should return an error if the constructor has one or more parameters")]
        public void Apply_ConstructorOutOfRange()
        {
            // arrange
            ConstructorOutOfRangeEventSource eventSource = ConstructorOutOfRangeEventSource.Log;
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

        [Fact(DisplayName = "Apply: Should return a success if only one private constructor without parameters exist")]
        public void Apply_Success()
        {
            // arrange
            ConstructorEventSource eventSource = ConstructorEventSource.Log;
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