using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode997
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(FindJudge(2, new int[][] { new int[] { 1, 2}}));
        }

        public int FindJudge(int n, int[][] trust)
        {

            var trustTo = new Dictionary<int, List<int>>();
            var trustedBy = new Dictionary<int, List<int>>();
            for (int i = 0; i < trust.Length; i++)
            {
                var list = trustTo.GetValueOrDefault(trust[i][0], new List<int>());
                list.Add(trust[i][1]);
                trustTo[trust[i][0]] = list;

                list = trustedBy.GetValueOrDefault(trust[i][1], new List<int>());
                list.Add(trust[i][0]);
                trustedBy[trust[i][1]] = list;
            }

            for (int i = 1; i <= n; i++)
            {
                if ((!trustTo.ContainsKey(i) || trustTo[i] == null || trustTo[i].Count == 0)
                    && trustedBy.ContainsKey(i)
                    && trustedBy[i] != null
                    && trustedBy[i].Count == n - 1)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
