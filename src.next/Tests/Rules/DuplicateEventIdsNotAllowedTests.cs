using ChilliCream.Logging.Analyzer.Rules;
using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests.Rules
{
    public class DuplicateEventIdsNotAllowedTests
        : EventSourceRuleTestBase<DuplicateEventIdsNotAllowed>
    {
        protected override DuplicateEventIdsNotAllowed CreateRule(IRuleSet ruleSet)
        {
            return new DuplicateEventIdsNotAllowed(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error result")]
        public void Apply_Error()
        {
            // arrange
            DuplicateEventIdEventSource eventSource = new DuplicateEventIdEventSource();
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
            OneEventEventSource eventSource = new OneEventEventSource();
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