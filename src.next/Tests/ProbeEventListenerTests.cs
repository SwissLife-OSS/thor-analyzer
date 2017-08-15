using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests
{
    public class ProbeEventListenerTests
    {
        [Fact(DisplayName = "EnqueueAsync: Should enqueue events in the right order")]
        public async Task EnqueueAsync()
        {
            using (ProbeEventListener listener = new ProbeEventListener())
            {
                // arrange
                listener.EnableEvents(MultipleEventsEventSource.Log, EventLevel.Verbose);

                // act
                MultipleEventsEventSource.Log.WithKeywords("1");
                MultipleEventsEventSource.Log.WithTags("2");

                // assert
                await Task.Delay(200).ConfigureAwait(false);

                EventWrittenEventArgs[] events = (EventWrittenEventArgs[])listener.OrderdEvents;

                events.Should().HaveCount(2);
                events[0].EventName.Should().Be("WithKeywords");
                events[1].EventName.Should().Be("WithTags");
            }
        }
    }
}