nusing System.Text;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        text = text.ToLower();
        var strings = text.Split(new char[] { '.', '!', '?', ';', ':', ')', '(', }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var str = new StringBuilder();
        foreach (var s in strings)
        {
            var listOf = new List<string>();
            foreach (var item in s)
            {
                if (item.Equals('\'') || char.IsLetter(item))
                    str.Append(item);
                else
                {
                    if (str.ToString().Length != 0)
                    {
                        listOf.Add(str.ToString());
                        str.Clear();
                    }
                }
            }
            if (str.ToString().Length > 0)
            {
                var semi = str.ToString();
                listOf.Add(semi);
            }
            str.Clear();
            if (listOf.Count != 0)
            {
                sentencesList.Add(listOf);
            }
        }
        return sentencesList;
    }
}