# EventSourceAnalyzer
[![release](https://img.shields.io/github/release/ChilliCream/EventSourceAnalyzer.svg)](https://github.com/ChilliCream/EventSourceAnalyzer/releases) [![package](https://img.shields.io/nuget/v/ChilliCream.Tracing.Analyzer.svg)](https://www.nuget.org/packages/ChilliCream.Tracing.Analyzer) [![license](https://img.shields.io/github/license/ChilliCream/EventSourceAnalyzer.svg)](https://github.com/ChilliCream/EventSourceAnalyzer/blob/master/LICENSE) [![build](https://img.shields.io/appveyor/ci/rstaib/EventSourceAnalyzer-322tu/master.svg)](https://ci.appveyor.com/project/rstaib/eventsourceanalyzer-322tu) [![tests](https://img.shields.io/appveyor/tests/rstaib/EventSourceAnalyzer-322tu/master.svg)](https://ci.appveyor.com/project/rstaib/eventsourceanalyzer-322tu) [![coverage](https://img.shields.io/coveralls/ChilliCream/EventSourceAnalyzer.svg)](https://coveralls.io/github/ChilliCream/EventSourceAnalyzer?branch=master)

An analyzer for *ETW* (Event Tracing for Windows) event sources which helps reveal frequent mistakes and also guides in best practices rules.

## Getting Started

Getting started in two steps. Install the *NuGet* package and start writing code. That´s it.

### Install Package

Before we start we have to decide which package would be the right choice for our project. If we work with the `System.Diagnostics.Tracing` namespace in combination with *.Net* 4.6.1 or higher, we might be well adviced by using the `ChilliCream.Tracing.Analyzer` package. But if we use the `Microsoft.Diagnostics.Tracing` namespace in combination with *.Net* 4.5.1 or higher instead, we should choose the `ChilliCream.Tracing.Analyzer.Legacy` package.

In summary, can be stated that newer projects using the `System.Diagnostics.Tracing` namespace should install the `ChilliCream.Tracing.Analyzer` package. See the following Powershell script.

```powershell
Install-Package ChilliCream.Tracing.Analyzer
```

Therefore, older projects using the `Microsoft.Diagnostics.Tracing` namespace should install the `ChilliCream.Tracing.Analyzer.Legacy` package. See the following Powershell script.

```powershell
Install-Package ChilliCream.Tracing.Analyzer.Legacy
```

### Basic Example

After installing the package of our choice, we can start writing code. The simplest way is the following tercet.

```csharp
MyCustomEventSource eventSource = new MyCustomEventSource();
EventSourceAnalyzer analyzer = new EventSourceAnalyzer();
Report report = analyzer.Inspect(eventSource);
```

### Automated Test Example

If you are familiar with *xunit* see how event sources can be tested automatically. Works with other test frameworks as well of course.

```csharp
[Fact(DisplayName = "Should not contain any error")]
public void Analyze()
{
    // Arrange
    MyCustomEventSource eventSource = MyCustomEventSource.Log;
    EventSourceAnalyzer analyzer = new EventSourceAnalyzer();

    // Act
    Report report = analyzer.Inspect(eventSource);

    // Assert
    Assert.False(report.HasErrors);
}
```

## Checkout the EventSourceGenerator

In most scenarios we recommend to use the `ChilliCream.Tracing.Generator` to generate event sources automatically. Thus, the critical parts are generated in an efficient way by the generator. Click [here](https://github.com/ChilliCream/EventSourceGenerator) to get more information about the *EventSourceGenerator*.