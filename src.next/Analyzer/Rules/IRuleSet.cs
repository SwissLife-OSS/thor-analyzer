using System.Collections.Generic;

namespace ChilliCream.Logging.Analyzer.Rules
{
    public interface IRuleSet
    {
        IEnumerable<IRule> Rules { get; }
    }
}