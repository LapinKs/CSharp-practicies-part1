using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private Dictionary<string, Dictionary<int, List<int>>> dictionary
            = new Dictionary<string, Dictionary<int, List<int>>>();
    public void Add(int id, string documentText)
    {
        var words = documentText.Split(new[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' });
        var pos = 0;
        foreach (var word in words)
        {
            if (!dictionary.ContainsKey(word))
                dictionary.Add(word, new Dictionary<int, List<int>>());
            if (!dictionary[word].ContainsKey(id))
                dictionary[word].Add(id, new List<int>());
            dictionary[word][id].Add(pos);
            pos += 1 + word.Length;
        }
    }

    public List<int> GetIds(string word)
    {
        var result = new List<int>();
        if (dictionary.ContainsKey(word))
        {
            foreach (var item in dictionary[word].Keys)
            {
                result.Add(item);
            }
        }
        return result;
    }

    public List<int> GetPositions(int id, string word) =>
        dictionary.ContainsKey(word) && dictionary[word].ContainsKey(id) ?
        dictionary[word][id] : new List<int>();


    public void Remove(int id)
    {
        var semi = dictionary.Keys.ToList();
        foreach (var word in semi)
            if (dictionary[word].ContainsKey(id))
                dictionary[word].Remove(id);
    }
}