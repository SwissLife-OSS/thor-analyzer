using ChilliCream.Logging.Analyzer.Rules;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests
{
    public class EventSourceAnalyzerTests
    {
        [Fact(DisplayName = "Inspect: Should return an error for duplicate event identifiers.")]
        public void Inspect_DuplicateEventId()
        {
            // arrange
            DuplicateEventIdEventSource eventSource = new DuplicateEventIdEventSource();
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            IEnumerable<IResult> results = analyzer.Inspect(eventSource);

            // assert
            results.Should().HaveCount(2);
            results.First().Should().BeOfType<Error>();
        }
    }
}