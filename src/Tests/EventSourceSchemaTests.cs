using FluentAssertions;
using System;
using Xunit;

namespace ChilliCream.Tracing.Analyzer.Tests
{
    public class EventSourceSchemaTests
    {
        [Fact(DisplayName = "Constructor: Should throw an argument exception for providerId")]
        public void Constructor_ProviderIdEmpty()
        {
            // arrange
            Guid providerId = Guid.Empty;
            string providerName = "Provider";

            // act
            Action throwException = () => new EventSourceSchema(providerId, providerName);

            // assert
            throwException.ShouldThrow<ArgumentException>("providerId");
        }

        [Fact(DisplayName = "Constructor: Should throw an argument null exception for providerName")]
        public void Constructor_ProviderNameNull()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            string providerName = null;

            // act
            Action throwException = () => new EventSourceSchema(providerId, providerName);

            // assert
            throwException.ShouldThrowNull("providerName");
        }

        [Fact(DisplayName = "Constructor: Should initialize an event source schema")]
        public void Constructor_Success()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            string providerName = "Provider";

            // act
            EventSourceSchema schema = new EventSourceSchema(providerId, providerName);

            // assert
            schema.ProviderId.Should().Be(providerId);
            schema.ProviderName.Should().Be(providerName);
            schema.Events.Should().BeNull();
        }
    }
}