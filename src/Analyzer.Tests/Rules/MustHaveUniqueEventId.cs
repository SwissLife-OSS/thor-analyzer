using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class MustHaveUniqueEventIdTests
        : EventSourceRuleTestBase<MustHaveUniqueEventId>
    {
        protected override MustHaveUniqueEventId CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveUniqueEventId(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if event source has non-unique event identifiers")]
        public void Apply_NonUniqueEventId()
        {
            // arrange
            NonUniqueEventIdEventSource eventSource = new NonUniqueEventIdEventSource();
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

        [Fact(DisplayName = "Apply: Should return a success if event source has unique event identifiers")]
        public void Apply_UniqueEventId()
        {
            // arrange
            UniqueEventIdEventSource eventSource = new UniqueEventIdEventSource();
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