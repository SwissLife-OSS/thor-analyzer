using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;
using Xunit;

namespace Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility.Tests
{
    public class EventSourceAnalyzerTests
    {
        [Fact]
        public void InspectEmptyEventSource()
        {
            // arrange
            EmptyEventSource emptyEventSource = EmptyEventSource.Log;

            // act
            EventSourceAnalyzer.InspectAll(emptyEventSource);
        }
    }
}
