using ChilliCream.Logging.Analyzer.Rules;
using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests.Rules
{
    public class ConstructionExceptionNotAllowedTests
        : EventSourceRuleTestBase<ConstructionExceptionNotAllowed>
    {
        protected override ConstructionExceptionNotAllowed CreateRule(IRuleSet ruleSet)
        {
            return new ConstructionExceptionNotAllowed(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error result")]
        public void Apply_Error()
        {
            // arrange
            ConstructionExceptionEventSource eventSource = ConstructionExceptionEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSourceSchema schema = reader.Read();
            IRuleSet ruleSet = new Mock<IRuleSet>().Object;
            IEventSourceRule rule = CreateRule(ruleSet);

            // act
            IResult result = rule.Apply(schema, eventSource);

            // assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Error>();
            ((Error)result).Details.Should().HaveCount(1);
        }

        [Fact(DisplayName = "Apply: Should return a success result")]
        public void Apply_Success()
        {
            // arrange
            NoConstructionExceptionEventSource eventSource = NoConstructionExceptionEventSource.Log;
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