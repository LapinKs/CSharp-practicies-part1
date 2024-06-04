using System.Collections.Generic;
namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var semiRes = new Dictionary<string, Dictionary<string, int>>();
        foreach (var sentence in text)
        {
            for (int i = 0; i < sentence.Count - 1; i++)
            {
                FrequencyPlus(sentence[i], sentence[i + 1], semiRes);
                if (i < sentence.Count - 2)
                {
                    FrequencyPlus(sentence[i] + " " + sentence[i + 1], sentence[i + 2], semiRes);
                }
            }
        }
        foreach (var item in semiRes.Keys)
            result[item] = GetFrequent(semiRes[item]);
        return result;
    }


    public static void FrequencyPlus(string first, string second, Dictionary<string, Dictionary<string, int>> dict)
    {
        if (dict.ContainsKey(first) == false)
            dict[first] = new Dictionary<string, int>();

        if (dict[first].ContainsKey(second))
            dict[first][second]++;
        else
            dict[first][second] = 1;

    }

    public static string GetFrequent(Dictionary<string, int> secondWordDict)
    {
        var str = "";
        int max = 0;
        foreach (var secondWord in secondWordDict.Keys)
        {
            if (string.CompareOrdinal(secondWord, str) < 0 && max == secondWordDict[secondWord] || max < secondWordDict[secondWord])
            {
                max = secondWordDict[secondWord];
                str = secondWord;
            }
        }
        return str;
    }
}