using Thor.Analyzer.Rules;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace Thor.Analyzer.Tests
{
    public class ReportTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for providerName")]
        public void Constructor_ProviderNameNull()
        {
            // arrange
            string providerName = null;
            IRuleSet[] ruleSets = new IRuleSet[0];
            IResult[] results = new IResult[0];

            // act
            Action throwException = () => new Report(providerName, ruleSets, results);

            // assert
            throwException.ShouldThrowNull("providerName");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for ruleSets")]
        public void Constructor_RuleSetsNull()
        {
            // arrange
            string providerName = "Provider";
            IRuleSet[] ruleSets = null;
            IResult[] results = new IResult[0];

            // act
            Action throwException = () => new Report(providerName, ruleSets, results);

            // assert
            throwException.ShouldThrowNull("ruleSets");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for results")]
        public void Constructor_ResultsNull()
        {
            // arrange
            string providerName = "Provider";
            IRuleSet[] ruleSets = new IRuleSet[0];
            IResult[] results = null;

            // act
            Action throwException = () => new Report(providerName, ruleSets, results);

            // assert
            throwException.ShouldThrowNull("results");
        }

        [Fact(DisplayName = "Constructor: Should initialize a report")]
        public void Constructor_Success()
        {
            // arrange
            string providerName = "Provider";
            IRuleSet[] ruleSets = new IRuleSet[] { new Mock<IRuleSet>().Object };
            IResult[] results = new IResult[]
            {
                new Error(new Mock<IRule>().Object, "Reason"),
                new Success(new Mock<IRule>().Object)
            };

            // act
            Report report = new Report(providerName, ruleSets, results);

            // assert
            report.ProviderName.Should().Be(providerName);
            report.RuleSets.Should().HaveCount(1);
            report.HasErrors.Should().BeTrue();
            report.Errors.Should().HaveCount(1);
        }
    }
}