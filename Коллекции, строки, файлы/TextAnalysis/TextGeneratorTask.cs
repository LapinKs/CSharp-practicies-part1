using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
        {
            var builder = new StringBuilder(phraseBeginning);
            for (int i = 0; i < wordsCount; i++)
            {
                string[] splitWords = builder.ToString().Split(' ');
                if (splitWords.Length >= 2 && nextWords.ContainsKey(splitWords[splitWords.Length - 2] + " " + splitWords[splitWords.Length - 1]))
                    builder.Append(" " + nextWords[splitWords[splitWords.Length - 2] + " " + splitWords[splitWords.Length - 1]]);

                else if (splitWords.Length >= 1 && nextWords.ContainsKey(splitWords[splitWords.Length - 1]))
                    builder.Append(" " + nextWords[splitWords[splitWords.Length - 1]]);
                else
                    break;
            }
            return builder.ToString();
        }
    }
}