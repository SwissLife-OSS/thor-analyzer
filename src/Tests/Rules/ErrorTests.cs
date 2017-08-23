using ChilliCream.Tracing.Analyzer.Rules;
using Moq;
using System;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests.Rules
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
            Action validate = () => new Error(rule, reason);

            // assert
            validate.ShouldThrowArgumentNull("rule");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for reason")]
        public void Constructor_ReasonNull()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = null;

            // act
            Action validate = () => new Error(rule, reason);

            // assert
            validate.ShouldThrowArgumentNull("reason");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for details")]
        public void Constructor_DetailsNull()
        {
            // arrange
            IRule rule = new Mock<IRule>().Object;
            string reason = "Reason";
            string[] details = null;

            // act
            Action validate = () => new Error(rule, reason, details);

            // assert
            validate.ShouldThrowArgumentNull("details");
        }
    }
}