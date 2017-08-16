using ChilliCream.Logging.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Logging.Analyzer.Tests
{
    public class SchemaReaderTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for eventSource")]
        public void Constructor_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;

            // act
            Action validate = () => new SchemaReader(eventSource);

            // assert
            validate.ShouldThrow<ArgumentNullException>()
                .Where(e => e.ParamName == "eventSource")
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "Read: Should return a schema")]
        public void Read()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSourceSchema schema = reader.Read();

            // assert
            schema.Events.Should().HaveCount(1);

            foreach (EventSchema eventSchema in schema.Events)
            {
                eventSchema.EventSource.Should().Be(schema);
            }
        }

        [Fact(DisplayName = "ReadEvent: Should return an event schema")]
        public void ReadEvent()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSchema eventSchema = reader.ReadEvent(1);

            // assert
            eventSchema.ShouldBe(1, "Foo", new[] { "bar" });
        }

        [Fact(DisplayName = "ReadEvent: Should throw an argument out of range exception")]
        public void ReadEvent_OutOfRange()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            Action validate = () => reader.ReadEvent(0);

            // assert
            validate.ShouldThrow<ArgumentOutOfRangeException>()
                .Where(e => e.ParamName == "eventId" && e.Message.StartsWith(ExceptionMessages.EventIdMustBeGreaterZero))
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ReadEvent: Should return null when passing an invalid id")]
        public void ReadEvent_UnknownEventId()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSchema schema = reader.ReadEvent(5);

            // assert
            schema.Should().BeNull();
        }

        [Fact(DisplayName = "ReadEvent: Should return an event with specified keywords")]
        public void ReadEvent_WithKeywords()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSchema eventSchema = reader.ReadEvent(3);

            // assert
            eventSchema.ShouldBe(3, "WithKeywords", EventLevel.Informational, EventOpcode.Info,
                MultipleEventsEventSource.Keywords.Diagnostic, new[] { "bar" }, 0);
        }

        [Fact(DisplayName = "ReadEvent: Should return an event with specified level")]
        public void ReadEvent_WithLevel()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSchema eventSchema = reader.ReadEvent(4);

            // assert
            eventSchema.ShouldBe(4, "WithLevel", EventLevel.Verbose, EventOpcode.Info,
                EventKeywords.None, new[] { "bar" }, 0);
        }

        [Fact(DisplayName = "ReadEvent: Should return an event with specified opcode")]
        public void ReadEvent_WithOpcode()
        {
            // arrange
            MultipleEventsEventSource eventSource = MultipleEventsEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            EventSchema eventSchema = reader.ReadEvent(6);

            // assert
            eventSchema.ShouldBe(6, "WithOpcode", EventLevel.Informational, EventOpcode.Receive,
                EventKeywords.None, new[] { "bar" }, 0);
        }
    }
}