using Thor.Analyzer.Rules;
using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class MustHaveValidNameTests
        : EventSourceRuleTestBase<MustHaveValidName>
    {
        protected override MustHaveValidName CreateRule(IRuleSet ruleSet)
        {
            return new MustHaveValidName(ruleSet);
        }

        [Fact(DisplayName = "Apply: Should return an error if name is invalid")]
        public void Apply_NameInvalid()
        {
            // arrange
            InvalidNameEventSource eventSource = InvalidNameEventSource.Log;
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

        [Fact(DisplayName = "Apply: Should return a success if name is valid")]
        public void Apply_ValidName()
        {
            // arrange
            ValidNameEventSource eventSource = ValidNameEventSource.Log;
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