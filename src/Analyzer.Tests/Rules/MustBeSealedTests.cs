using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class MustBeSealedTests
        : EventSourceRuleTestBase<MustBeSealed>
    {
        protected override MustBeSealed CreateRule(IRuleSet ruleSet)
        {
            return new MustBeSealed(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if event source is not sealed")]
        public void Apply_NotSealed()
        {
            // arrange
            NotSealedEventSource eventSource = new NotSealedEventSource();
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

        [Fact(DisplayName = "Apply: Should return a success if event source is sealed")]
        public void Apply_Sealed()
        {
            // arrange
            SealedEventSource eventSource = new SealedEventSource();
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