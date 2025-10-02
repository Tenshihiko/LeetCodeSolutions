

using System.Text;

namespace LeetCode966;

public class Solution
{
    public void Run()
    {
        string[] wordlist = ["KiTe", "kite", "hare", "Hare"];
        string[] queries = ["kite", "Kite", "KiTe", "Hare", "HARE", "Hear", "hear", "keti", "keet", "keto"];
        var result = Spellchecker(wordlist, queries);
    }

    public string[] Spellchecker(string[] wordlist, string[] queries)
    {
        var dictionary = PrepareDictionary(wordlist);

        var result = new string[queries.Length];

        for (int i = 0; i < queries.Length; i++)
        {
            var hash = GetHashCode(queries[i]);
            var lower = queries[i].ToLower();
            
            if (dictionary.ContainsKey(hash))
            {
                if (dictionary[hash].dict.ContainsKey(lower))
                {
                    if (dictionary[hash].dict[lower].set.Contains(queries[i]))
                    {
                        result[i] = queries[i];
                    }
                    else
                    {
                        result[i] = dictionary[hash].dict[lower].first;
                    }
                }
                else
                {
                    result[i] = dictionary[hash].first;
                }
            }
            else
            {
                result[i] = "";
            }
        }

        return result.ToArray();
    }

    private Dictionary<string, (string first, Dictionary<string, (string first, HashSet<string> set)> dict)> PrepareDictionary(string[] wordlist)
    {
        var dictionary = new Dictionary<string, (string first, Dictionary<string, (string first, HashSet<string> set)> dict)>();
        for (int i = 0; i < wordlist.Length; i++)
        {
            var hash = GetHashCode(wordlist[i]);

            var lower = wordlist[i].ToLower();

            if (!dictionary.ContainsKey(hash))
            {
                dictionary[hash] = (wordlist[i], new Dictionary<string, (string first, HashSet<string> set)>());
                dictionary[hash].dict[lower] = (wordlist[i], new HashSet<string> { wordlist[i] });
            }
            else
            {
                if (!dictionary[hash].dict.ContainsKey(lower))
                {
                    dictionary[hash].dict[lower] = (wordlist[i], new HashSet<string> { wordlist[i] });
                }
                else
                {
                    dictionary[hash].dict[lower].set.Add(wordlist[i]);
                }
            }
        }

        return dictionary;
    }

    private string GetHashCode(string v)
    {
        var stringBuilder = new StringBuilder();
        foreach (var c in v)
        {
            if (IsVowel(c))
            {
                stringBuilder.Append('#');
            }
            else
            {
                stringBuilder.Append(char.ToLower(c));
            }
        }

        return stringBuilder.ToString();
    }

    private bool IsVowel(char c)
    {
        return "aeiouAEIOU".IndexOf(c) >= 0;
    }
}

