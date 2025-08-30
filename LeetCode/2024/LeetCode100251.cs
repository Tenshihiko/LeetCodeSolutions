using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100251
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(ShortestSubstrings(["vbb", "grg", "lexn", "oklqe", "yxav"]));
        }

        public string[] ShortestSubstrings(string[] arr)
        {

            var subs = new SortedDictionary<string, HashSet<int>>(Comparer<string>.Create((x, y) =>
                (x.Length == y.Length) ? x.CompareTo(y) : x.Length.CompareTo(y.Length))
            );

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    for (int k = j; k < arr[i].Length; k++)
                    {
                        var sub = arr[i].Substring(j, k - j + 1);

                        var list = subs.GetValueOrDefault(sub, new HashSet<int>());
                        list.Add(i);
                        subs[sub] = list;
                    }
                }
            }

            var res = new string[arr.Length];
            Array.Fill(res, "");

            foreach (var sub in subs)
            {
                if (sub.Value.Count == 1)
                {
                    var val = sub.Value.First();
                    if (res[val] == "")
                    {
                        res[val] = sub.Key;
                    }
                }
            }

            return res;
        }
    }
}
