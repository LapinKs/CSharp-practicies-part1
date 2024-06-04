using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete;

internal class AutocompleteTask
{
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var i = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (i < phrases.Count && phrases[i].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[i];
        else return null;
    }

    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var actual = Math.Min(GetCountByPrefix(phrases, prefix), count);
        var start = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var result = new string[actual];
        Array.Copy(phrases.ToArray(), start, result, 0, actual);
        return result;
    }

    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count) -
            LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        var result = AutocompleteTask.GetTopByPrefix(new List<string>() { }, "a", 1234);
        Assert.AreEqual(new string[0], result);
    }

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        var testList = new List<string>() { "gcs", "gxs", "fdas" };
        var result = AutocompleteTask.GetTopByPrefix(testList, "", testList.Count);
        Assert.AreEqual(testList.ToArray(), result);
    }

    [Test]
    public void CountByPrefix_FirstBasedTest()
    {
        var testList = new List<string>() { "bank", "barabka", "barabashka", "baran", "bars" };
        var result = AutocompleteTask.GetTopByPrefix(testList, "bar", 4);
        Assert.AreEqual(new[] { "barabka", "barabashka", "baran", "bars" }, result);
    }

    [Test]
    public void CountByPrefix_SecondBasedTest()
    {
        var testList = new List<string>() { "mem", "mvd", "mvv", "mvk" };
        var result = AutocompleteTask.GetTopByPrefix(testList, "mv", 3);
        Assert.AreEqual(new[] { "mvd", "mvv", "mvk" }, result);
    }
    [Test]
    public void CountByPrefix_BasedTest()
    {
        var testList = new List<string>() { "mem", "mvd" };
        var result = AutocompleteTask.GetTopByPrefix(testList, "mv", 4);
        Assert.AreEqual(new[] { "mvd" }, result);
    }
}