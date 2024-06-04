using NUnit.Framework;
using System.Linq;
using System.Text;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
    [TestCase("''", 0, "", 2)]
    [TestCase("'aboltus'", 0, "aboltus", 8)]
    [TestCase("\\'micro\\\\'test", 0, "micro\\", 9)]
    [TestCase("\"Next test\\\'\"", 0, "Next test'", 13)]
    [TestCase("'a'", 0, "a", 3)]
    [TestCase("'not neeedd", 0, "not neeedd", 11)]
    [TestCase("\'\\\\test\'\'", 0, "\\test", 8)]
    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
    }
}

class QuotedFieldTask
{
    public static Token ReadQuotedField(string line, int startIndex)
    {
        var result = new StringBuilder();
        var i = startIndex + 1;
        for (; i < line.Length && line[i] != line[startIndex]; i++)
        {
            if (line[i].Equals('\\')) i++;
            result.Append(line[i]);
        }
        if (i < line.Length) i++;
        return new Token(result.ToString(), startIndex, i - startIndex);
    }
}