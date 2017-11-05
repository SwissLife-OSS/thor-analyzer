using Thor.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Xunit;

namespace Thor.Analyzer.Tests
{
    public class SchemaReaderTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for eventSource")]
        public void Constructor_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;

            // act
            Action throwException = () => new SchemaReader(eventSource);

            // assert
            throwException.ShouldThrowNull("eventSource");
        }

        [Fact(DisplayName = "ParseSchemaAsync: Should return a schema")]
        public async Task ParseSchemaAsync()
        {
            // arrange
            string filePath = ".\\SchemaReader.xml";
            string manifest = await FileHelper.ReadAllTextAsync(filePath).ConfigureAwait(false);

            // act
            EventSourceSchema eventSchema = SchemaReader.ParseSchema(manifest);

            // assert
            eventSchema.Should().NotBeNull();
        }

        [Fact(DisplayName = "Read: Should return a schema")]
        public void Read()
        {
            // arrange
            OneEventEventSource eventSource = OneEventEventSource.Log;
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
            OneEventEventSource eventSource = OneEventEventSource.Log;
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
            OneEventEventSource eventSource = OneEventEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);

            // act
            Action throwException = () => reader.ReadEvent(0);

            // assert
            throwException.ShouldThrow<ArgumentOutOfRangeException>()
                .Where(e => e.ParamName == "eventId" && e.Message
                    .StartsWith(ExceptionMessages.EventIdMustBeGreaterZero))
                .Should()
                .NotBeNull();
        }

        [Fact(DisplayName = "ReadEvent: Should return null when passing an invalid id")]
        public void ReadEvent_UnknownEventId()
        {
            // arrange
            OneEventEventSource eventSource = OneEventEventSource.Log;
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
                MultipleEventsEventSource.Keywords.Diagnostic, new[] { "bar" });
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
                EventKeywords.None, new[] { "bar" });
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
            eventSchema.ShouldBe(6, "WithOpcode", EventLevel.Informational, null,
                EventOpcode.Receive, EventKeywords.None, 0, new[] { "bar" });
        }
    }
}