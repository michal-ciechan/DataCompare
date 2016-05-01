# DataCompare
Compare Lists / Tabular data with rich set of results for reporting.

# Code Samples Preface

Most of the following code sample will be using the following `TestData.Lists.Default` to minimize lines of code, followed by self explanatory (hopefully) fluent calls.

```csharp
public static class Lists
{
    public static List<List<string>> Default => new List<List<string>>
    {
        new List<string> {"Key", "Value"},
        new List<string> {"1", "Test"},
    };
}
```

If you see `left` and `right` used somewhere with no definition, these are set to the above `Lists.Default`.

```csharp
left = Lists.Default;
right = Lists.Default;
```
# Row Collection



# Compare Result Information

### Same

Returns true if no differences where found between compared lists.

```csharp
var diff = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(left, diff);

res1.Same.Should().BeTrue();
res2.Same.Should().BeFalse();
```

### Left only

Contains the rows whose keys were present only in the left list.

```csharp
var leftExtra = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(leftExtra, right);

res1.LeftOnly.Should().BeEmpty();
res2.LeftOnly.ShouldBeEquivalentTo(leftExtra.Last());
```

### Right Only

Contains the rows whose keys were present only in the right list.

```csharp
var rightExtra = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(left, rightExtra);

res1.RightOnly.Should().BeEmpty();
res2.RightOnly.ShouldBeEquivalentTo(rightExtra.Last());
```

# Config

The following are the default `DataComparerConfig` settings

## HasHeaders

Sets wether or not the first row has headers.

```csharp
//Default
HasHeaders = true;
```

## Key Columns

```csharp
//Default
Keys = new HashSet<string>
{
    "Key"
}
```

## Skipped Columns

```csharp
//Default
Skip = new HashSet<string>
{
    "ID", "Skip"
}
```
