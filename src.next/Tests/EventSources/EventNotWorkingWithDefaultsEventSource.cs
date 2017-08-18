﻿using System;
using System.Diagnostics.Tracing;

namespace ChilliCream.Logging.Analyzer.Tests.EventSources
{
    [EventSource(Name = "EventNotWorkingWithDefaults")]
    public sealed class EventNotWorkingWithDefaultsEventSource
       : EventSource
    {
        private EventNotWorkingWithDefaultsEventSource() { }

        public static EventNotWorkingWithDefaultsEventSource Log = new EventNotWorkingWithDefaultsEventSource();

        [Event(1)]
        public void Foo(string bar)
        {
            if (string.IsNullOrWhiteSpace(bar))
            {
                throw new ArgumentNullException(nameof(bar));
            }
        }
    }
}