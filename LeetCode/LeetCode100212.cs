using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100212
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(CountPrefixSuffixPairs(["bb", "b", "a", "aa", "bb"]));
        }

        private bool isPrefixAndSuffix(string s1, string s2)
        {
            if (s2.Length < s1.Length)
            {
                return false;
            }
            var n = s1.Length;
            var m = s2.Length;

            for (int i = 0; i < n; i++)
            {
                if (s1[i] != s2[i] || s1[n - 1 - i] != s2[m - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }

        public int CountPrefixSuffixPairs(string[] words)
        {
            var pairs = 0;

            for (int i = 0; i < words.Length; i++)
            {
                for (int j = i + 1; j < words.Length; j++)
                {
                    if (isPrefixAndSuffix(words[i], words[j]))
                    {
                        pairs++;
                    }
                }
            }

            return pairs;
        }
    }
}
