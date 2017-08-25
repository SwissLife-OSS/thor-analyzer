using ChilliCream.Tracing.Analyzer.Rules;
using FluentAssertions;
using Moq;
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
            Action throwException = () => new Success(rule);

            // assert
            throwException.ShouldThrowNull("rule");
        }

        [Fact(DisplayName = "Constructor: Should not throw any exception")]
        public void Constructor_Success()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;

            // act
            Success success = new Success(rule);

            // assert
            success.Rule.Should().Be(rule);
        }
    }
}