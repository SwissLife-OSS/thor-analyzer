using System;
using System.Diagnostics.Tracing;
using System.Reflection;

namespace ChilliCream.Tracing.Analyzer
{
    internal static class EventSchemaExtensions
    {
        public static bool HasRelatedActivityId(this EventSchema schema, ParameterInfo[] eventParameters)
        {
            if (eventParameters.Length > 0 &&
                eventParameters[0].ParameterType == typeof(Guid) &&
                string.Equals(eventParameters[0].Name, "relatedActivityId", StringComparison.Ordinal) &&
                (schema.Opcode == EventOpcode.Send || schema.Opcode == EventOpcode.Receive))
            {
                return true;
            }

            return false;
        }
    }
}