using FluentAssertions;
using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests
{
    internal static class EventSchemaExtensions
    {
        public static void ShouldBe(this EventSchema schema, int id, string name, string[] payloads) =>
            schema.ShouldBe(id, name, EventLevel.Informational, EventOpcode.Info, EventKeywords.None, payloads, 0);

        public static void ShouldBe(this EventSchema schema, int id, string name, EventLevel level,
            EventOpcode opcode, EventKeywords keywords,  string[] payloads, int version)
        {
            schema.Should().NotBeNull();
            schema.Id.Should().Be(id);
            schema.Name.Should().Be(name);
            schema.Level.Should().Be(level);
            schema.Opcode.Should().Be(opcode);
            schema.Keywords.Should().Be(keywords);
            schema.Version.Should().Be(version);
            schema.Payload.Should().Contain(payloads);
            schema.Payload.Should().HaveCount(payloads.Length);
        }
    }
}