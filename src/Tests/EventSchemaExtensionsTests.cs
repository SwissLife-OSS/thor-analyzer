using ChilliCream.Tracing.Analyzer.Tests.EventSources;
using FluentAssertions;
using System;
using System.Diagnostics.Tracing;
using System.Reflection;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class EventSchemaExtensionsTests
    {
        [Fact(DisplayName = "HasRelatedActivityId: Should throw an argument null exception for schema")]
        public void HasRelatedActivityId_SchemaNull()
        {
            // arrange
            EventSchema schema = null;
            ParameterInfo[] eventParameters = new ParameterInfo[0];

            // act
            Action throwException = () => schema.HasRelatedActivityId(eventParameters);

            // assert
            throwException.ShouldThrowNull("schema");
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should throw an argument null exception for eventParameters")]
        public void HasRelatedActivityId_EventParametersNull()
        {
            // arrange
            EventSourceSchema eventSourceSchema = new EventSourceSchema(Guid.NewGuid(), "Provider");
            EventSchema schema = new EventSchema(eventSourceSchema, 0, "Name", EventLevel.Error,
                EventTask.None, "Task", EventOpcode.Info, EventKeywords.None, 0, new string[0]);
            ParameterInfo[] eventParameters = null;

            // act
            Action throwException = () => schema.HasRelatedActivityId(eventParameters);

            // assert
            throwException.ShouldThrowNull("parameters");
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return false because eventParameters length is 0")]
        public void HasRelatedActivityId_EventParametersEmpty()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(6);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("Sixth")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return false because first parameter is not Guid")]
        public void HasRelatedActivityId_FirstParameterNotGuid()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(1);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("First")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return false because first parameter is not named relatedActivityId")]
        public void HasRelatedActivityId_FirstParameterNotRelatedId()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(2);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("Second")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return false because Opcode is not Send or Receive")]
        public void HasRelatedActivityId_FirstParameterWrongOpcode()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(3);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("Third")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return true because Opcode is Send")]
        public void HasRelatedActivityId_FirstParameterSend()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(4);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("Forth")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "HasRelatedActivityId: Should return true because Opcode is Receive")]
        public void HasRelatedActivityId_FirstParameterReceive()
        {
            // arrange
            RelatedActivityIdEventSource eventSource = RelatedActivityIdEventSource.Log;
            SchemaReader reader = new SchemaReader(eventSource);
            EventSchema schema = reader.ReadEvent(5);
            ParameterInfo[] eventParameters = eventSource.GetType().GetMethod("Fifth")
                .GetParameters();

            // act
            bool result = schema.HasRelatedActivityId(eventParameters);

            // assert
            result.Should().BeTrue();
        }
    }
}