using System.Collections.Generic;

namespace ChilliCream.Tracing.Analyzer.Rules
{
    public interface IRuleSet
    {
        IEnumerable<IRule> Rules { get; }
    }
}