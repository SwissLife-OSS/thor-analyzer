using ChilliCream.Logging.Analyzer.Rules;
using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests
{
    public class EventSourceAnalyzerTests
    {
        [Fact(DisplayName = "Inspect: Should return an error for duplicate event identifiers")]
        public void Inspect_DuplicateEventId()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

            // act
            IEnumerable<IResult> results = analyzer.Inspect(eventSource);

            // assert
            results.Should().HaveCount(analyzer.RuleSets.SelectMany(r => r.Rules).Count());
            results.All(r => r.GetType().IsAssignableFrom(typeof(Success))).Should().BeTrue();
        }
    }
}