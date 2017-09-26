using ChilliCream.Tracing.Analyzer.Tests.EventSources;
using System;
using System.Diagnostics.Tracing;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class EventSourceExtensionsTests
    {
        [Fact(DisplayName = "GetMethodFromSchema: Should throw an argument null exception for " +
            "eventSource")]
        public void GetMethodFromSchema_EventSourceNull()
        {
            // arrange
            EventSource eventSource = null;
            EventSchema schema = null;

            // act
            Action throwException = () => eventSource.GetMethodFromSchema(schema);

            // assert
            throwException.ShouldThrowNull("eventSource");
        }

        [Fact(DisplayName = "GetMethodFromSchema: Should throw an argument null exception for " +
            "eventSource")]
        public void GetMethodFromSchema_SchemaNull()
        {
            // arrange
            EventSource eventSource = MultipleEventsEventSource.Log;
            EventSchema schema = null;

            // act
            Action throwException = () => eventSource.GetMethodFromSchema(schema);

            // assert
            throwException.ShouldThrowNull("schema");
        }
    }
}