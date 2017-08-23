using ChilliCream.Tracing.Analyzer.Rules;
using System;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests.Rules
{
    public class SuccessTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for rule")]
        public void Constructor_RuleNull()
        {
            // arrange
            IRule rule = null;

            // act
            Action validate = () => new Success(rule);

            // assert
            validate.ShouldThrowArgumentNull("rule");
        }
    }
}