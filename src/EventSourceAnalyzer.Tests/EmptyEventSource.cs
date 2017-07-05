using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility.Tests
{
    [EventSource(Name = "EmptyEventSource")]
    public sealed class EmptyEventSource
        : EventSource
    {
        public static readonly EmptyEventSource Log = new EmptyEventSource();

        private EmptyEventSource() { }

        [Event(1)]
        public void InvalidEvent()
        {
            if (IsEnabled())
            {
                WriteEvent(2);
            }
        }
    }

}