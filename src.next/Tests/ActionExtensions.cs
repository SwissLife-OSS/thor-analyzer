using FluentAssertions;
using System;

namespace ChilliCream.Logging.Analyzer.Tests
{
    internal static class ActionExtensions
    {
        public static void ShouldThrowArgumentNull(this Action execute, string expectedParameterName)
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
                .ShouldThrow<ArgumentNullException>()
                .Where(e => e.ParamName == expectedParameterName)
                .Should()
                .NotBeNull();
        }
    }
}