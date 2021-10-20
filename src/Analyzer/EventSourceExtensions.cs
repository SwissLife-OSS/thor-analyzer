using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace Thor.Analyzer
{
    internal static class EventSourceExtensions
    {
        private const BindingFlags _bindings = 
            BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod | 
            BindingFlags.NonPublic | BindingFlags.Public;

        public static MethodInfo GetMethodFromSchema(this EventSource eventSource,
            EventSchema schema)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            return eventSource
                .GetMethods()
                .SingleOrDefault(m =>
                    m.IsEvent(schema.Id)) ?? eventSource.GetType().GetMethod(schema.TaskName, _bindings);
        }

        public static IEnumerable<MethodInfo> GetMethods(this EventSource eventSource)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            return eventSource.GetType().GetMethods(_bindings);
        }

        public static bool IsEvent(this MethodInfo method, int eventId)
        {
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return method.GetEvent()?.EventId == eventId;
        }

        public static EventAttribute GetEvent(this MethodInfo method)
            => method.GetCustomAttribute<EventAttribute>();

        public static bool TryInvokeMethod(this EventSource eventSource, EventSchema eventSchema,
            MethodInfo method, object[] values, out string exceptionMessage)
        {
            exceptionMessage = null;

            try
            {
                method.Invoke(eventSource, values);

                return true;
            }
            catch (Exception ex)
            {
                exceptionMessage = ex.Message;
            }

            return false;
        }
    }
}