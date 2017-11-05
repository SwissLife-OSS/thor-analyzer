using FluentAssertions;
using System.Diagnostics.Tracing;

namespace Thor.Analyzer.Tests
{
    internal static class EventSchemaExtensions
    {
        public static void ShouldBe(this EventSchema schema, int id, string name, string[] payload) =>
            schema.ShouldBe(id, name, EventLevel.Informational, EventOpcode.Info,
                EventKeywords.None, payload);

        public static void ShouldBe(this EventSchema schema, int id, string name, EventLevel level,
            EventOpcode opcode, EventKeywords keywords, string[] payload) =>
                schema.ShouldBe(id, name, level, name, opcode, keywords, 0, payload);

        public static void ShouldBe(this EventSchema schema, int id, string name, EventLevel level,
            string taskName, EventOpcode opcode, EventKeywords keywords, int version,
            string[] payloads)
        {
            schema.Should().NotBeNull();
            schema.Id.Should().Be(id);
            schema.Keywords.Should().Be(keywords);
            schema.Level.Should().Be(level);
            schema.Name.Should().Be(name);
            schema.Opcode.Should().Be(opcode);
            schema.Payload.Should().ContainInOrder(payloads);
            schema.Payload.Should().HaveCount(payloads.Length);
            schema.TaskName.Should().Be(taskName);
            schema.Version.Should().Be(version);
        }

        public static void ShouldBe(this EventSchema schema, int id, string name, EventLevel level,
            EventTask task, string taskName, EventOpcode opcode, EventKeywords keywords,
            int version, string[] payloads)
        {
            schema.ShouldBe(id, name, level, taskName, opcode, keywords, version, payloads);
            schema.Task.Should().Be(task);
        }
    }
}