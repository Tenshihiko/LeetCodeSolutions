using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode60
{
    public class Solution
    { 
        public void Run()
        {
           Console.WriteLine(GetPermutation(3, 3) == "213");
           Console.WriteLine(GetPermutation(4, 9) == "2314");
           Console.WriteLine(GetPermutation(3, 1) == "123");
           Console.WriteLine(GetPermutation(1, 1) == "1");
        }

        public string GetPermutation(int n, int k)
        {
            if (n == 1 && k == 1) return "1";

            var res = "";
            var used = new bool[n];

            var nfact = Enumerable.Range(1, n - 1).Aggregate(1, (p, item) => p * item);

            for (int i = 0; i < n; i++)
            {
                var x = k / nfact;

                while (used[x])
                {
                    x = (x + 1) % n;
                }

                used[x] = true;
                res += (x + 1);
                k %= nfact;
                nfact  = i + 1 < n ? nfact / (n - i - 1) : 1;
            }

            return res;
        }
    }
}
