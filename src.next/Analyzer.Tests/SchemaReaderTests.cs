using System;
using System.Collections.Generic;
using System.Linq;
using ChilliCream.Tracing.Schema;
using FluentAssertions;
using Microsoft.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Tracing.Tests
{
    public class SchemaReaderTests
    {
        [Fact]
        public void GetSchema()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();

            // act
            EventSourceSchema schema = eventSource.GetSchema();

            // assert
            schema.Events.Should().HaveCount(1);

            foreach (EventSchema eventSchema in schema.Events.Values)
            {
                eventSchema.EventSource.Should().Be(schema);
            }

            EventSchema fooSchema = schema.Events[1];
            fooSchema.TaskName.Should().Be("Foo");
            fooSchema.EventSource.Name.Should().Be("OneEvent");
            fooSchema.Payload.Should().HaveCount(1);
            fooSchema.Payload.Should().Contain(new[] { "bar" });
        }

        [Fact]
        public void GetSchema_DuplicateEventIds()
        {
            // arrange
            DuplicateEventIdEventSource eventSource = new DuplicateEventIdEventSource();

            // act
            EventSourceSchema schema = eventSource.GetSchema();

            // assert
            schema.Errors.Should().HaveCount(1);

            EventSourceSchemaError error = schema.Errors.First();
            error.Code.Should().Be(EventSourceSchemaErrorCodes.DuplicateEventIds);
            error.Events.Should().HaveCount(2);

            foreach (EventSchema eventSchema in error.Events)
            {
                eventSchema.EventSource.Should().Be(schema);
            }

            schema.Events.Should().HaveCount(1);

            EventSchema validSchema = schema.Events[2];
            validSchema.TaskName.Should().Be("Valid");
        }

        [Fact]
        public void GetEventSchema()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();

            // act
            EventSchema eventSchema = eventSource.GetEventSchema(1);

            // assert
            eventSchema.Should().NotBeNull();
            eventSchema.TaskName.Should().Be("Foo");
            eventSchema.EventSource.Name.Should().Be("OneEvent");
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }

        [Fact]
        public void GetEventSchema_UnknownEventId()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();

            // act
            Action getEventSchema = () => eventSource.GetEventSchema(5);

            // assert
            getEventSchema.ShouldThrow<KeyNotFoundException>();
        }

        [Fact]
        public void TryGetEventSchema()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();

            // act
            EventSchema eventSchema;
            bool success = eventSource.TryGetEventSchema(1, out eventSchema);

            // assert
            success.Should().BeTrue();
            eventSchema.Should().NotBeNull();
            eventSchema.Name.Should().Be("Foo");
            eventSchema.EventSource.Name.Should().Be("OneEvent");
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }

        [Fact]
        public void TryGetEventSchema_UnknownEventId()
        {
            // arrange
            OneEventEventSource eventSource = new OneEventEventSource();

            // act
            EventSchema eventSchema;
            bool success = eventSource.TryGetEventSchema(5, out eventSchema);

            // assert
            success.Should().BeFalse();
            eventSchema.Should().BeNull();
        }

        [Fact]
        public void GetEventWithKeywords()
        {
            // arrange
            MultipleEventsEventSource eventSource = new MultipleEventsEventSource();

            // act
            EventSchema eventSchema = eventSource.GetEventSchema(3);

            // assert
            eventSchema.Should().NotBeNull();
            eventSchema.Id.Should().Be(3);
            eventSchema.Name.Should().Be("WithKeywords");
            eventSchema.Keywords.Should().Be(EventKeywords.AuditFailure);
            eventSchema.KeywordsDescription.Should().BeNull();
            eventSchema.Level.Should().Be(EventLevel.Informational);
            eventSchema.Opcode.Should().Be(EventOpcode.Info);
            eventSchema.Version.Should().Be(0);
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }

        [Fact]
        public void GetEventWithLevel()
        {
            // arrange
            MultipleEventsEventSource eventSource = new MultipleEventsEventSource();

            // act
            EventSchema eventSchema = eventSource.GetEventSchema(4);

            // assert
            eventSchema.Should().NotBeNull();
            eventSchema.Id.Should().Be(4);
            eventSchema.Name.Should().Be("WithLevel");
            eventSchema.Keywords.Should().Be(EventKeywords.None);
            eventSchema.KeywordsDescription.Should().BeNull();
            eventSchema.Level.Should().Be(EventLevel.Verbose);
            eventSchema.Opcode.Should().Be(EventOpcode.Info);
            eventSchema.Version.Should().Be(0);
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }

        [Fact]
        public void GetEventWithOpcode()
        {
            // arrange
            MultipleEventsEventSource eventSource = new MultipleEventsEventSource();

            // act
            EventSchema eventSchema = eventSource.GetEventSchema(6);

            // assert
            eventSchema.Should().NotBeNull();
            eventSchema.Id.Should().Be(6);
            eventSchema.Name.Should().Be("WithOpcode");
            eventSchema.Keywords.Should().Be(EventKeywords.None);
            eventSchema.KeywordsDescription.Should().BeNull();
            eventSchema.Level.Should().Be(EventLevel.Informational);
            eventSchema.Opcode.Should().Be(EventOpcode.Receive);
            eventSchema.Version.Should().Be(0);
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }
    }

    [EventSource(Name = "MultipleEvents")]
    public class MultipleEventsEventSource
       : EventSource
    {
        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(3, Keywords = EventKeywords.AuditFailure)]
        public void WithKeywords(string bar)
        {
            WriteEvent(3, bar);
        }

        [Event(4, Level = EventLevel.Verbose)]
        public void WithLevel(string bar)
        {
            WriteEvent(4, bar);
        }

        [Event(6, Opcode = EventOpcode.Receive)]
        public void WithOpcode(string bar)
        {
            WriteEvent(6, bar);
        }

        [Event(7, Tags = (EventTags)5)]
        public void WithTags(string bar)
        {
            WriteEvent(7, bar);
        }

        [Event(8, Task = (EventTask)7)]
        public void WithTask(string bar)
        {
            WriteEvent(8, bar);
        }
    }
}
