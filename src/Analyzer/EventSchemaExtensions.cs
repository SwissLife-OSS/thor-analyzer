using System;
using System.Reflection;

#if LEGACY
using Microsoft.Diagnostics.Tracing;
#else
using System.Diagnostics.Tracing;
#endif

namespace ChilliCream.Tracing.Analyzer
{
    internal static class EventSchemaExtensions
    {
        public static bool HasRelatedActivityId(this EventSchema schema,
            ParameterInfo[] parameters)
        {
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (parameters.Length > 0 && parameters[0].ParameterType == typeof(Guid) &&
                string.Equals(parameters[0].Name, "relatedActivityId", StringComparison.Ordinal) &&
                (schema.Opcode == EventOpcode.Send || schema.Opcode == EventOpcode.Receive))
            {
                return true;
            }

            return false;
        }
    }
}