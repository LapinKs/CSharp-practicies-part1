using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Linq;
using System;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }
        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("\\", new[] { "\\" })]
        [TestCase("a'b'c'd'dddd'r'", new[] { "a", "b", "c", "d", "dddd", "r" })]
        [TestCase("' ''a'''", new[] { " ", "a", "" })]
        [TestCase("mini\"test", new[] { "mini", "test" })]
        [TestCase("\\\"bcdr\"", new[] { "\\", "bcdr" })]
        [TestCase("", new string[0])]
        [TestCase(" ", new string[] { })]
        [TestCase("'one\\'two'", new string[] { "one'two" })]
        [TestCase("\"tree\\\"four\"", new string[] { "tree\"four" })]
        [TestCase("   beleboba   ", new string[] { "beleboba" })]
        [TestCase("\"test'test'\"", new string[] { "test'test'" })]
        [TestCase("'inside\"of\"'", new string[] { "inside\"of\"" })]
        [TestCase("\"fake escape\\\\\"", new string[] { "fake escape\\" })]
        [TestCase("'beleboba  ", new string[] { "beleboba  " })]

        public static void RunTests(string input, string[] expectedOutput)
        {
            // Тело метода изменять не нужно
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string text)
        {
            var outPut = new List<Token>();
            int pos = 0;
            while (pos < text.Length)
            {
                var token = new Token(null, 0, 0);
                if ((text[pos] == '\"') || (text[pos] == '\''))
                    token = ReadQuotedField(text, pos);
                else if (text[pos] != ' ')
                {
                    var res = new StringBuilder();
                    int i = pos;
                    for (; ((i < text.Length) && text[i] != ' ' && text[i] != '"' && text[i] != '\''); i++)
                        res.Append(text[i]);
                    token = new Token(res.ToString(), pos, res.ToString().Length);
                    res.Clear();
                }
                if (token.Value == null)
                    pos++;
                else
                {
                    pos += token.Length;
                    outPut.Add(token);
                }
            }
            return outPut;
        }

        private static Token ReadField(string line, int startIndex)
        {
            return new Token(line, 0, line.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}