using System.Diagnostics.Tracing;

namespace ChilliCream.Tracing.Analyzer.Tests.EventSources
{
    [EventSource(Name = "Schema")]
    public sealed class SchemaEventSource
       : EventSource
    {
        private SchemaEventSource() { }

        public static readonly SchemaEventSource Log = new SchemaEventSource();

        [Event(1, Level = EventLevel.LogAlways, Opcode = EventOpcode.DataCollectionStart)]
        public void LogAlways(string bar)
        {
            WriteEvent(1, bar);
        }

        [Event(2, Level = EventLevel.Critical)]
        public void Critical(string bar)
        {
            WriteEvent(2, bar);
        }

        [Event(3, Level = EventLevel.Error)]
        public void Error(string bar)
        {
            WriteEvent(3, bar);
        }

        [Event(4, Level = EventLevel.Warning)]
        public void Warning(string bar)
        {
            WriteEvent(4, bar);
        }

        [Event(5, Level = EventLevel.Informational)]
        public void Informational(string bar)
        {
            WriteEvent(5, bar);
        }

        [Event(6, Level = EventLevel.Verbose)]
        public void Verbose(string bar)
        {
            WriteEvent(6, bar);
        }

        [Event(10, Opcode = EventOpcode.DataCollectionStart)]
        public void DataCollectionStart(string bar)
        {
            WriteEvent(10, bar);
        }

        [Event(11, Opcode = EventOpcode.DataCollectionStop)]
        public void DataCollectionStop(string bar)
        {
            WriteEvent(11, bar);
        }

        [Event(12, Opcode = EventOpcode.Extension)]
        public void Extension(string bar)
        {
            WriteEvent(12, bar);
        }

        [Event(13, Opcode = EventOpcode.Receive)]
        public void Receive(string bar)
        {
            WriteEvent(13, bar);
        }

        [Event(14, Opcode = EventOpcode.Reply)]
        public void Reply(string bar)
        {
            WriteEvent(14, bar);
        }

        [Event(15, Opcode = EventOpcode.Resume)]
        public void Resume(string bar)
        {
            WriteEvent(15, bar);
        }

        [Event(16, Opcode = EventOpcode.Send)]
        public void Send(string bar)
        {
            WriteEvent(16, bar);
        }

        [Event(17, Opcode = EventOpcode.Start)]
        public void Start(string bar)
        {
            WriteEvent(17, bar);
        }

        [Event(18, Opcode = EventOpcode.Stop)]
        public void Stop(string bar)
        {
            WriteEvent(18, bar);
        }

        [Event(19, Opcode = EventOpcode.Suspend)]
        public void Suspend(string bar)
        {
            WriteEvent(19, bar);
        }
    }
}