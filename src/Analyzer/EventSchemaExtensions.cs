using System;
using System.Diagnostics.Tracing;
using System.Reflection;

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