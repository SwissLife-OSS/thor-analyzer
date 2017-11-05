using FluentAssertions;
using System;
using Xunit;
using Xunit.Sdk;

namespace Thor.Analyzer.Tests
{
    public class ActionExtensionsTests
    {
        #region ShouldThrow

        [Fact(DisplayName = "ShouldThrow: Should throw an argument null exception for execute")]
        public void ShouldThrow_ExecuteNull()
        {
            // arrange
            const string parameterName = "param";
            Action execute = null;

            // act
            Action throwException = () => execute.ShouldThrow<ArgumentException>(parameterName);

            // assert
            throwException.ShouldThrowNull("execute");
        }

        [Fact(DisplayName = "ShouldThrow: Should throw an argument null exception for expectedParameterName")]
        public void ShouldThrow_ExpectedParameterNameNull()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrow<ArgumentException>(null);

            // assert
            throwException.ShouldThrowNull("expectedParameterName");
        }

        [Fact(DisplayName = "ShouldThrow: Should throw a Xunit exception")]
        public void ShouldThrow_NoExceptionAtAll()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrow<ArgumentException>("param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrow: Should successfully catch the exception")]
        public void ShouldThrow_NothingWrong()
        {
            // arrange
            const string parameterName = "param";
            Action execute = () => throw new ArgumentException("message", parameterName);

            // act
            execute.ShouldThrow<ArgumentException>(parameterName);

            // assert
            // no exception should be thrown
        }

        [Fact(DisplayName = "ShouldThrow: Should throw a Xunit exception")]
        public void ShouldThrow_WrongException()
        {
            // arrange
            Action execute = () => { throw new ArgumentNullException("sadas"); };

            // act
            Action throwException = () => execute.ShouldThrow<ArgumentException>("param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ShouldThrow: Should throw a Xunit exception")]
        public void ShouldThrow_WrongParameterName()
        {
            // arrange
            Action execute = () => { throw new ArgumentException("message", "param"); };

            // act
            Action throwException = () => execute.ShouldThrow<ArgumentException>("wrong-param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        #endregion

        #region ShouldThrowArgumentNull

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw an argument null exception for source")]
        public void ShouldThrowArgumentNull_ExecuteNull()
        {
            // arrange
            const string parameterName = "param";
            Action execute = null;

            // act
            Action throwException = () => execute.ShouldThrowNull(parameterName);

            // assert
            throwException.ShouldThrowNull("execute");
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw an argument null exception for expectedParameterName")]
        public void ShouldThrowArgumentNull_ExpectedParameterNameNull()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrowNull(null);

            // assert
            throwException.ShouldThrowNull("expectedParameterName");
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw a Xunit exception")]
        public void ShouldThrowArgumentNull_NoExceptionAtAll()
        {
            // arrange
            Action execute = () => { };

            // act
            Action throwException = () => execute.ShouldThrowNull("param");

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
            execute.ShouldThrowNull(parameterName);

            // assert
            // no exception should be thrown
        }

        [Fact(DisplayName = "ShouldThrowArgumentNull: Should throw a Xunit exception")]
        public void ShouldThrowArgumentNull_WrongException()
        {
            // arrange
            Action execute = () => { throw new ArgumentException(); };

            // act
            Action throwException = () => execute.ShouldThrowNull("param");

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
            Action throwException = () => execute.ShouldThrowNull("wrong-param");

            // assert
            throwException
                .ShouldThrow<XunitException>()
                .Should()
                .NotBeNull();
        }

        #endregion
    }
}