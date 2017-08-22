using FluentAssertions;
using System;
using Xunit;
using Xunit.Sdk;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class ActionExtensionsTests
    {
        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw an argument null exception for source")]
        public void ShouldThrowArgumentNull_ExecuteNull()
        {
            // arrange
            const string parameterName = "param";
            Action execute = null;

            // act
            Action throwException = () => execute.ShouldThrowArgumentNull(parameterName);

            // assert
            throwException
                .ShouldThrow<ArgumentNullException>()
                .Where(e => e.ParamName == "execute")
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw an argument null exception for expectedParameterName")]
        public void ShouldThrowArgumentNull_ExpectedParameterNameNull()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrowArgumentNull(null);

            // assert
            throwException
                .ShouldThrow<ArgumentNullException>()
                .Where(e => e.ParamName == "expectedParameterName")
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw a Xunit exception")]
        public void ShouldThrowArgumentNull_NoExceptionAtAll()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrowArgumentNull("param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should successfully catch the exception")]
        public void ShouldThrowArgumentNull_NothingWrong()
        {
            // arrange
            const string parameterName = "param";
            Action execute = () => throw new ArgumentNullException(parameterName);

            // act
            execute.ShouldThrowArgumentNull(parameterName);

            // assert
            // no exception should be thrown
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw a Xunit exception")]
        public void ShouldThrowArgumentNull_WrongException()
        {
            // arrange
            Action execute = () => { throw new ArgumentException(); };

            // act
            Action throwException = () => execute.ShouldThrowArgumentNull("param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw a Xunit exception")]
        public void ShouldThrowArgumentNull_WrongParameterName()
        {
            // arrange
            Action execute = () => { throw new ArgumentNullException("param"); };

            // act
            Action throwException = () => execute.ShouldThrowArgumentNull("wrong-param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }
    }
}