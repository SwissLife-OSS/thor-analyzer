using System;
using System.Collections.Generic;
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
            schema.Events.Should().HaveCount(2);

            EventSchema fooSchema = schema.Events[1];
            fooSchema.TaskName.Should().Be("Foo");
            fooSchema.EventSource.Name.Should().Be("OneEvent");
            fooSchema.Payload.Should().HaveCount(1);
            fooSchema.Payload.Should().Contain(new[] { "bar" });
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
        public void GetEventWithChannel()
        {
            // arrange
            MultipleEventsEventSource eventSource = new MultipleEventsEventSource();

            // act
            EventSchema eventSchema = eventSource.GetEventSchema(2);

            // assert
            eventSchema.Should().NotBeNull();
            eventSchema.Name.Should().Be("WithChannel");
            eventSchema.Id.Should().Be(2);
            eventSchema.TaskName.Should().Be("WithChannel");
            eventSchema.EventSource.Name.Should().Be("MultipleEvents");
            eventSchema.Payload.Should().HaveCount(1);
            eventSchema.Payload.Should().Contain(new[] { "bar" });
        }
    }

    [EventSource(Name = "OneEvent")]
    public class OneEventEventSource
        : EventSource
    {
        [Event(1)]
        public void Foo(string bar)
        {
            WriteEvent(1, bar);
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

        [Event(2, Channel = EventChannel.Admin)]
        public void WithChannel(string bar)
        {
            WriteEvent(2, bar);
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
        
        [Event(5, Message = "ABC {0}")]
        public void WithMessage(string bar)
        {
            WriteEvent(5, bar);
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
