using FluentAssertions;
using System;
using Xunit;

namespace Thor.Analyzer.Tests
{
    public class SchemaCacheTests
    {
        #region TryAdd

        [Fact(DisplayName = "TryAdd: Should add a schema to the cache")]
        public void TryAdd()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            EventSourceSchema schema = new EventSourceSchema(providerId, providerId.ToString());
            SchemaCache cache = new SchemaCache();

            // act
            bool added = cache.TryAdd(schema);

            // assert
            added.Should().BeTrue();
        }

        [Fact(DisplayName = "TryAdd: Should not add a schema to the cache if already cached")]
        public void TryAdd_ExistsAlready()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            EventSourceSchema schema = new EventSourceSchema(providerId, providerId.ToString());
            SchemaCache cache = new SchemaCache();
            cache.TryAdd(schema);

            // act
            bool added = cache.TryAdd(schema);

            // assert
            added.Should().BeFalse();
        }

        [Fact(DisplayName = "TryAdd: Should throw an argument null exception for eventSource")]
        public void TryAdd_SchemaNull()
        {
            // arrange
            SchemaCache cache = new SchemaCache();

            // act
            Action throwException = () => cache.TryAdd(null);

            // assert
            throwException.ShouldThrowNull("schema");
        }

        #endregion

        #region TryGet

        [Fact(DisplayName = "TryGet: Should get a schema from the cache")]
        public void TryGet()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            EventSourceSchema input = new EventSourceSchema(providerId, providerId.ToString());
            SchemaCache cache = new SchemaCache();
            cache.TryAdd(input);

            // act
            bool exists = cache.TryGet(providerId, out EventSourceSchema output);

            // assert
            exists.Should().BeTrue();
            output.Should().NotBeNull();
        }

        [Fact(DisplayName = "TryGet: Should return null if not exists")]
        public void TryGet_ExistsAlready()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            SchemaCache cache = new SchemaCache();

            // act
            bool exists = cache.TryGet(providerId, out EventSourceSchema result);

            // assert
            exists.Should().BeFalse();
        }

        [Fact(DisplayName = "TryGet: Should throw an argument null exception for eventSource")]
        public void TryGet_SchemaNull()
        {
            // arrange
            SchemaCache cache = new SchemaCache();

            // act
            Action throwException = () => cache.TryGet(Guid.Empty, out EventSourceSchema result);

            // assert
            throwException.ShouldThrow<ArgumentException>()
                .Where(e => e.ParamName == "providerId" && e.Message
                    .StartsWith(ExceptionMessages.ProviderIdMayNotBeEmpty))
                .Should()
                .NotBeNull();
        }

        #endregion

        #region Clear

        [Fact(DisplayName = "Clear: Should clear the cache")]
        public void Clear()
        {
            // arrange
            Guid providerId = Guid.NewGuid();
            EventSourceSchema input = new EventSourceSchema(providerId, providerId.ToString());
            SchemaCache cache = new SchemaCache();
            cache.TryAdd(input);

            // act
            cache.Clear();

            // assert
            cache.TryGet(providerId, out EventSourceSchema output).Should().BeFalse();
            output.Should().BeNull();
        }

        #endregion
    }
}