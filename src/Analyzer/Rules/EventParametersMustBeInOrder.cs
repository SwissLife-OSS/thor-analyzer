using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    /// <summary>
    /// A rule which checks if the <see cref="EventSource"/> matches the schema.
    /// </summary>
    public class EventParametersMustBeInOrder
        : IEventRule
    {
        /// <summary>
        /// Initiates a new instance of the <see cref="EventParametersMustBeInOrder"/> class.
        /// </summary>
        /// <param name="ruleSet">A ruleset which is the parent of this rule.</param>
        public EventParametersMustBeInOrder(IRuleSet ruleSet)
        {
            if (ruleSet == null)
            {
                throw new ArgumentNullException(nameof(ruleSet));
            }

            RuleSet = ruleSet;
        }

        /// <inheritdoc/>
        public IRuleSet RuleSet { get; }

        /// <inheritdoc/>
        public IResult Apply(EventSchema schema, EventSource eventSource)
        {
            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }

            Type eventSourceType = eventSource.GetType();

            using (ProbeEventListener listener = new ProbeEventListener())
            {
                try
                {
                    listener.EnableEvents(eventSource, schema.Level, schema.Keywords);

                    MethodInfo method = eventSource.GetMethodFromSchema(schema);
                    ParameterInfo[] parameters = method.GetParameters();
                    object[] values = parameters
                        .Select(p => p.ParameterType.NotDefault())
                        .ToArray();
                    string exceptionMessage = null;
                    bool eventExecuted = (method != null &&
                        eventSource.TryInvokeMethod(schema, method, values, out exceptionMessage) &&
                        listener.OrderedEvents.Count() == 1);

                    if (eventExecuted)
                    {
                        string errorMessage = CheckPayloadOrder(schema, method, parameters,
                            listener.OrderedEvents.First());

                        if (errorMessage != null)
                        {
                            return new Error(this, errorMessage);
                        }
                    }
                    else
                    {
                        return new Error(this, "This rule could not be executed successfully, " +
                            "because the preconditions failed.");
                    }
                }
                finally
                {
                    listener.DisableEvents(eventSource);
                }
            }

            return new Success(this);
        }

        private static string CheckPayloadOrder(EventSchema schema, MethodInfo method,
            ParameterInfo[] parameters, EventWrittenEventArgs eventData)
        {
            if (parameters.Length > 0)
            {
                int parameterOffset = (schema.HasRelatedActivityId(parameters)) ? 1 : 0;
                int parameterCount = parameters.Length - parameterOffset;

                for (int i = 0; i < parameterCount; i++)
                {
                    if (parameters[i + parameterOffset].Name != eventData.PayloadNames[i])
                    {
                        return $"The parameter name '{parameters[i].Name}' defined in the event " +
                            $"'{method.Name}' does not match the order in WriteEvent function.";
                    }
                }
            }

            return null;
        }
    }
}