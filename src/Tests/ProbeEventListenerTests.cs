using ChilliCream.Tracing.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
using System.Diagnostics.Tracing;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class ProbeEventListenerTests
    {
        private readonly ITestOutputHelper _output;

        public ProbeEventListenerTests(ITestOutputHelper output)
        {
            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            _output = output;
        }

        [Fact(DisplayName = "Enqueue: Should enqueue events in the right order")]
        public void Enqueue()
        {
            using (ProbeEventListener listener = new ProbeEventListener())
            {
                // arrange
                listener.EnableEvents(ProbeEventListenerEventSource.Log, EventLevel.Verbose);
                listener.EventSourceCreated += (sender, eventArgs) =>
                    _output.WriteLine($"Created event source: {eventArgs.EventSource.Name}");

                // act
                ProbeEventListenerEventSource.Log.Foo("1");
                ProbeEventListenerEventSource.Log.Bar("2");
                ProbeEventListenerEventSource.Log.Foo("3");

                // assert
                EventWrittenEventArgs[] events = (EventWrittenEventArgs[])listener.OrderdEvents;

                events.Should().HaveCount(3);
                events[0].EventName.Should().Be("Foo");
                events[0].Payload.Single().Should().Be("1");
                events[1].EventName.Should().Be("Bar");
                events[1].Payload.Single().Should().Be("2");
                events[2].EventName.Should().Be("Foo");
                events[2].Payload.Single().Should().Be("3");
            }
        }
    }
}