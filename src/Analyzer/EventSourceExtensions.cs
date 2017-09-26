using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;

namespace ChilliCream.Tracing.Analyzer
{
    internal static class EventSourceExtensions
    {
        private const BindingFlags _bindings = BindingFlags.Instance | BindingFlags.DeclaredOnly |
            BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public;

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

            return eventSource.GetType().GetMethods(_bindings).SingleOrDefault(m =>
                m.IsEvent(schema.Id)) ?? eventSource.GetType().GetMethod(schema.TaskName, _bindings);
        }

        private static bool IsEvent(this MethodInfo method, int eventId)
        {
            return method.GetCustomAttribute<EventAttribute>() != null &&
                method.GetCustomAttribute<EventAttribute>().EventId == eventId;
        }

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