using FluentAssertions;
using System;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    internal static class ActionExtensions
    {
        public static void ShouldThrow<TArgumentException>(this Action execute, string expectedParameterName)
            where TArgumentException : ArgumentException
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            if (string.IsNullOrWhiteSpace(expectedParameterName))
            {
                throw new ArgumentNullException(nameof(expectedParameterName));
            }

            execute
                .ShouldThrowExactly<TArgumentException>()
                .Which.ParamName.Should().Be(expectedParameterName);
        }

        public static void ShouldThrowNull(this Action execute, string expectedParameterName) =>
            execute.ShouldThrow<ArgumentNullException>(expectedParameterName);
    }
}