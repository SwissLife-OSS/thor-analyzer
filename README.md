# EventSourceAnalyzer [![GitHub release](https://img.shields.io/github/release/ChilliCream/EventSourceAnalyzer.svg)](https://github.com/ChilliCream/EventSourceAnalyzer/releases) [![license](https://img.shields.io/github/license/ChilliCream/EventSourceAnalyzer.svg)](https://github.com/ChilliCream/EventSourceAnalyzer/blob/raf/dev/LICENSE)

An *ETW* EventSource analyzer build on *.Net Standard 2.0*.

---

*ETW* (Event Tracing for Windows) is a very powerful instrument for tracing. However it could be very time consuming and frustrating to create a custom Event Provider (EventSource). The reason for that is there are many rules which have to be satisfied. Even as a professional mistakes can be made easily. Here comes *EventSourceAnalyzer* which helps by analyzing EventSources to find any schema errors possible.

Some tools we used to build this library.

|                      |     |
| -------------------- | --- |
| Build with           | [![AppVeyor branch](https://img.shields.io/appveyor/ci/rstaib/EventSourceAnalyzer/master.svg)](https://ci.appveyor.com/project/rstaib/eventsourceanalyzer-322tu) [![Travis branch](https://img.shields.io/travis/ChilliCream/EventSourceAnalyzer/master.svg)](https://travis-ci.org/ChilliCream/EventSourceAnalyzer) |
| Quality ensured with | [![Coveralls](https://img.shields.io/coveralls/ChilliCream/EventSourceAnalyzer.svg)](https://coveralls.io/github/ChilliCream/EventSourceAnalyzer?branch=master) |
| Packaged with        | [![NuGet](https://img.shields.io/nuget/v/ChilliCream.Tracing.Analyzer.svg)](https://www.nuget.org/packages/ChilliCream.Tracing.Analyzer) |

## Getting Started

Getting started is explained in three steps.

1. Add the *NuGet* `ChilliCream.Tracing.Analyzer` to your project (e.g. your test project)

2. Create an instance of any EventSource and pass it to the analyzer's `Inspect` method

```csharp
MyCustomEventSource eventSource = new MyCustomEventSource();
EventSourceAnalyzer analyzer = new EventSourceAnalyzer();
IReport report = analyzer.Inspect(eventSource);
```

3. Check the report to see whats wrong