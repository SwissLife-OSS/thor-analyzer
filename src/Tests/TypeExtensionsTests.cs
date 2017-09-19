using FluentAssertions;
using System;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class TypeExtensionsTests
    {
        #region Default

        [Fact(DisplayName = "Default: Should throw an argument null exception for type")]
        public void Default_TypeNull()
        {
            // arrange
            Type type = null;

            // act
            Action throwException = () => type.Default();

            // assert
            throwException.ShouldThrowNull("type");
        }

        [Fact(DisplayName = "Default: Should return a default byte array value")]
        public void Default_ByteArray()
        {
            // arrange
            Type type = typeof(byte[]);

            // act
            object result = type.Default();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<byte[]>();
            ((byte[])result).Should().HaveCount(0);
        }

        [Fact(DisplayName = "Default: Should return a default integer value")]
        public void Default_Integer()
        {
            // arrange
            Type type = typeof(int);

            // act
            object result = type.Default();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<int>()
                .And.Be(0);
        }

        [Fact(DisplayName = "Default: Should return a default string value")]
        public void Default_String()
        {
            // arrange
            Type type = typeof(string);

            // act
            object result = type.Default();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<string>()
                .And.Be(string.Empty);
        }

        #endregion

        #region NotDefault

        [Fact(DisplayName = "NotDefault: Should throw an argument null exception for type")]
        public void NotDefault_TypeNull()
        {
            // arrange
            Type type = null;

            // act
            Action throwException = () => type.NotDefault();

            // assert
            throwException.ShouldThrowNull("type");
        }

        [Fact(DisplayName = "NotDefault: Should return a non default boolean value")]
        public void NotDefault_Boolean()
        {
            // arrange
            Type type = typeof(bool);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<bool>()
                .And.Be(true);
        }

        [Fact(DisplayName = "NotDefault: Should return a non default byte array value")]
        public void NotDefault_ByteArray()
        {
            // arrange
            Type type = typeof(byte[]);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<byte[]>();
            ((byte[])result).Should().ContainInOrder((byte)1, (byte)2, (byte)3);
        }

        [Fact(DisplayName = "NotDefault: Should return a non default datetime value")]
        public void NotDefault_DateTime()
        {
            // arrange
            Type type = typeof(DateTime);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<DateTime>()
                .And.NotBe(DateTime.MinValue)
                .And.NotBe(DateTime.MaxValue);
        }

        [Fact(DisplayName = "NotDefault: Should return a non default integer value")]
        public void NotDefault_Integer()
        {
            // arrange
            Type type = typeof(int);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<int>()
                .And.Be(1);
        }

        [Fact(DisplayName = "NotDefault: Should return a non default string value")]
        public void NotDefault_String()
        {
            // arrange
            Type type = typeof(string);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<string>()
                .And.Be("An arbitrary string value");
        }

        [Fact(DisplayName = "NotDefault: Should return a non default object value")]
        public void NotDefault_Object()
        {
            // arrange
            Type type = typeof(object);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<object>();
        }

        [Fact(DisplayName = "NotDefault: Should return a non default argument exception value")]
        public void NotDefault_ArgumentException()
        {
            // arrange
            Type type = typeof(ArgumentException);

            // act
            object result = type.NotDefault();

            // assert
            result.Should().NotBeNull()
                .And.BeOfType<ArgumentException>();
        }

        #endregion
    }
}