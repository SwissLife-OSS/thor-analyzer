using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
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
                EventWrittenEventArgs[] events = (EventWrittenEventArgs[])listener.OrderdEvents;
                Random random = new Random();
                int expectedCount = 2;
                int run = 0;

                while (events.Length != expectedCount && run++ < 50)
                {
                    await Task.Delay(random.Next(50, 100)).ConfigureAwait(false);
                }

                events.Should().HaveCount(expectedCount);
                events[0].EventName.Should().Be("WithKeywords");
                events[1].EventName.Should().Be("WithTags");
            }
        }
    }
}