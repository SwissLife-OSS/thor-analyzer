using ChilliCream.Logging.Analyzer.Rules;
using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests.Rules
{
    public class MustHaveSinglePrivateConstructorTests
        : EventSourceRuleTestBase<MustHaveSinglePrivateConstructor>
    {
        protected override MustHaveSinglePrivateConstructor CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveSinglePrivateConstructor(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error because constructor does not exist")]
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

        [Fact(DisplayName = "Apply: Should return an error because the constructor is not private")]
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

        [Fact(DisplayName = "Apply: Should return an error because the constructor is static")]
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

        [Fact(DisplayName = "Apply: Should return an error because there are multiple constructors")]
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

        [Fact(DisplayName = "Apply: Should return an error because the constructor at least one parameter")]
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

        [Fact(DisplayName = "Apply: Should return a success result")]
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