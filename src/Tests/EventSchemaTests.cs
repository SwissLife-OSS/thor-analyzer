using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace Thor.Analyzer.Tests
{
    public class EventSchemaTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument null exception for schema")]
        public void Constructor_SchemaNull()
        {
            // arrange
            EventSourceSchema schema = null;
            int eventId = 1;
            string eventName = "Name";
            EventLevel level = EventLevel.Warning;
            EventTask task = EventTask.None;
            string taskName = string.Empty;
            EventOpcode opcode = EventOpcode.Receive;
            EventKeywords keywords = EventKeywords.Sqm;
            int version = 1;
            string[] payload = new string[0];

            // act
            Action throwException = () => new EventSchema(schema, eventId, eventName, level, task,
                taskName, opcode, keywords, version, payload);

            // assert
            throwException.ShouldThrowNull("schema");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for name")]
        public void Constructor_EventNameNull()
        {
            // arrange
            EventSourceSchema schema = new EventSourceSchema(Guid.NewGuid(), "Provider");
            int eventId = 1;
            string eventName = null;
            EventLevel level = EventLevel.Warning;
            EventTask task = EventTask.None;
            string taskName = string.Empty;
            EventOpcode opcode = EventOpcode.Receive;
            EventKeywords keywords = EventKeywords.Sqm;
            int version = 1;
            string[] payload = new string[0];

            // act
            Action throwException = () => new EventSchema(schema, eventId, eventName, level, task,
                taskName, opcode, keywords, version, payload);

            // assert
            throwException.ShouldThrowNull("name");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for payload")]
        public void Constructor_PayloadNull()
        {
            // arrange
            EventSourceSchema schema = new EventSourceSchema(Guid.NewGuid(), "Provider");
            int eventId = 1;
            string eventName = "Name";
            EventLevel level = EventLevel.Warning;
            EventTask task = EventTask.None;
            string taskName = string.Empty;
            EventOpcode opcode = EventOpcode.Receive;
            EventKeywords keywords = EventKeywords.Sqm;
            int version = 1;
            string[] payload = null;

            // act
            Action throwException = () => new EventSchema(schema, eventId, eventName, level, task,
                taskName, opcode, keywords, version, payload);

            // assert
            throwException.ShouldThrowNull("payload");
        }

        [Fact(DisplayName = "Constructor: Should initialize an event schema")]
        public void Constructor_Success()
        {
            // arrange
            EventSourceSchema schema = new EventSourceSchema(Guid.NewGuid(), "Provider");
            int eventId = 1;
            string eventName = "Name";
            EventLevel level = EventLevel.Warning;
            EventTask task = EventTask.None;
            string taskName = string.Empty;
            EventOpcode opcode = EventOpcode.Receive;
            EventKeywords keywords = EventKeywords.Sqm;
            int version = 1;
            string[] payload = new string[0];

            // act
            EventSchema eventSchema = new EventSchema(schema, eventId, eventName, level, task,
                taskName, opcode, keywords, version, payload);

            // assert
            eventSchema.ShouldBe(eventId, eventName, level, task, taskName, opcode, keywords,
                version, payload);
        }
    }
}