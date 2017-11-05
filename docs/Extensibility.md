# Extensibility

The `Thor Analyzer` is designed with extensibility in mind. Creating your own rule sets and rules is easy. Just add your custom rule sets or completely replace the default ones.

## Add custom rule sets

Use `Add` to add a single rule set or `AddRange` for adding multiple rule sets.

```csharp
EventSoureAnalyzer anlayzer = new EventSoureAnalyzer();
analyzer.Add(new MyCustomRuleSet());
```

## Replace default rule sets

The default rule sets can be completely replaced by providing custom rule sets to the constructor.

*Warning: The `Thor Analyzer` brings a bunch of helpful rule sets with it. If you replace them by your own rule sets you have to take care of schema validation by your self.*

```csharp
IRuleSet[] ruleSets = new []
{
    new MyCustomRuleSet()
};
EventSoureAnalyzer anlayzer = new EventSoureAnalyzer(ruleSets);
```