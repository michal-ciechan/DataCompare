# DataCompare

Compare Lists / Tabular data with rich set of results for reporting.

# Table of Content

<!-- TOC depthFrom:1 depthTo:6 withLinks:1 updateOnSave:1 orderedList:0 -->

- [DataCompare](#datacompare)
- [Table of Content](#table-of-content)
- [Code Samples Preface](#code-samples-preface)
- [Compare Result Information](#compare-result-information)
	- [Same](#same)
	- [Left only](#left-only)
	- [Right Only](#right-only)
- [Row Collection](#row-collection)
- [Config](#config)
	- [HasHeaders](#hasheaders)
	- [Key Columns](#key-columns)
	- [Skipped Columns](#skipped-columns)

<!-- /TOC -->

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

# Compare Result Information

## Same

Returns true if no differences where found between compared lists.

```csharp
var diff = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(left, diff);

res1.Same.Should().BeTrue();
res2.Same.Should().BeFalse();
```

## Left only

Contains the rows whose keys were present only in the left list.

```csharp
var leftExtra = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(leftExtra, right);

res1.LeftOnly.Should().BeEmpty();
res2.LeftOnly.ShouldBeEquivalentTo(leftExtra.Last());
```

## Right Only

Contains the rows whose keys were present only in the right list.

```csharp
var rightExtra = Lists.Default.Add(2, "New");

var res1 = _comparer.Compare(left, right);
var res2 = _comparer.Compare(left, rightExtra);

res1.RightOnly.Should().BeEmpty();
res2.RightOnly.ShouldBeEquivalentTo(rightExtra.Last());
```

# Row Collection

'RowCollection' class represents a collection of rows, and parses first row as header, as well as finds the **Key**, **Skipped** and **Data** column indexes.

N.B. Uses DataComparerConfig.Default if no config is passed in.


# Config

The following are available settings on the `DataComparerConfig` settings class

## HasHeaders

**THIS IS NOT YET IMPLEMETNED. PLANNED FOR A FUTURE RELEASE!**

Sets wether or not the first row has headers.

```csharp
//Default
HasHeaders = true;
```

When parsing a collection with no headers, the comparer will try to convert any `Key`/`Skip` value into an `int`, and if successfulll and is within the range of the row length, it will use this as an index.


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
