using Thor.Analyzer.Rules;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Thor.Analyzer.Tests.Rules
{
    public class ErrorTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for rule")]
        public void Constructor_RuleNull()
        {
            // arrange
            IRule rule = null;
            string reason = "Reason";

            // act
            Action throwException = () => new Error(rule, reason);

            // assert
            throwException.ShouldThrowNull("rule");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for reason")]
        public void Constructor_ReasonNull()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = null;

            // act
            Action throwException = () => new Error(rule, reason);

            // assert
            throwException.ShouldThrowNull("reason");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for details")]
        public void Constructor_DetailsNull()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = "Reason";
            string[] details = null;

            // act
            Action throwException = () => new Error(rule, reason, details);

            // assert
            throwException.ShouldThrowNull("details");
        }

        [Fact(DisplayName = "Constructor: Should not throw any exception")]
        public void Constructor_Success()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = "Reason";

            // act
            Error error = new Error(rule, reason);

            // assert
            error.Details.Should().BeNull();
            error.Reason.Should().Be(reason);
            error.Rule.Should().Be(rule);
        }

        [Fact(DisplayName = "Constructor: Should not throw any exception with details")]
        public void Constructor_Success_Details()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = "Reason";
            string[] details = new string[] { "Details" };

            // act
            Error error = new Error(rule, reason, details);

            // assert
            error.Details.ToArray().Should().HaveCount(1)
                .And.ContainInOrder("Details");
            error.Reason.Should().Be(reason);
            error.Rule.Should().Be(rule);
        }
    }
}