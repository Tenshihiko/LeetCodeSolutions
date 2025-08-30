using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode76
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(MinWindow("ADOBECODEBANC", "ABC"));
        }

        public string MinWindow(string s, string t)
        {
            var m = s.Length;
            var n = t.Length;

            var dict = new Dictionary<char, int>();
            var notCovered = n;

            foreach (var ch in t)
            {
                dict[ch] = dict.GetValueOrDefault(ch, 0) + 1;
            }

            var shortest = m + 1;

            var start = -1;
            var end = -1;

            var i = -1;
            var j = -1;
            do
            {
                while (notCovered > 0 && j < m - 1)
                {
                    j++;

                    if (dict.ContainsKey(s[j]))
                    {
                        if (dict[s[j]] > 0)
                        {
                            notCovered--;
                        }
                        dict[s[j]]--;
                    }
                }

                if (notCovered > 0)
                {
                    break;
                }

                while (notCovered == 0 && i < j)
                {
                    i++;

                    if (dict.ContainsKey(s[i]))
                    {
                        if (dict[s[i]] >= 0)
                        {
                            notCovered++;
                        }
                        dict[s[i]]++;
                    }
                }

                if (j - i + 1 < shortest)
                {
                    shortest = j - i + 1;
                    start = i;
                    end = j;
                }
            } while (i < m);

            if (shortest == m + 1)
            {
                return "";
            }
            else
            {
                return s.Substring(start, shortest);
            }
        }
    }
}
