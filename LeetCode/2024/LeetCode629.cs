using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode629
{
    public class Solution
    {
        private int Calc(int n, int k)
        {
            if (k == 0)
            {
                return 1;
            }

            var maxK = (n - 1) * n / 2;
            if (k > maxK)
            {
                return 0;
            }

            if (k > maxK / 2)
            {
                return Calc(n, maxK - k);
            }

            return (Calc(n - 1, k) + Calc(n, k - 1) - (k >= n ? Calc(n - 1, k - n) : 0)) % 1000000007;

        }

        public int KInversePairs(int n, int k)
        {
            var m = new int[n+1, k+1];
            m[0, 0] = 1;

            for (int i = 1; i <= n; i++)
            {
                var maxJ = Math.Min(k, (n - 1) * n / 2);
                var midK = (n - 1) * n / 2;
                for (int j = 0; j <= maxJ; j++)
                {
                    if (j == 0)
                    {
                        m[i, j] = 1;
                        continue;
                    }

                    if (j > midK)
                    {
                        m[i, j] = 0;
                        continue;
                    }


                    if (j > midK / 2)
                    {
                        m[i, j] = m[i, midK - j];
                        continue;
                    }

                    m[i, j] = (m[i - 1, j] % 1000000007
                                + m[i, j - 1] % 1000000007
                                + 1000000007
                                - (j >= i ? m[i - 1, j - i] : 0) % 1000000007) % 1000000007;
                }
            }

            for (int i = 1; i <= n; i++)
            {
                Console.Write(i.ToString("D2") + ": ");
                for (int j = 0; j <= k; j++)
                {
                    Console.Write(m[i,j].ToString("D4") + " ");
                }
                Console.WriteLine();
            }

            return m[n, k];
        }

        public void Run()
        {
            var solution = new Solution();

            //for (int i = 1; i <= 10; i++)
            //{
            //    Console.Write(i.ToString("D2") + ": ");
            //    for (int j = 0; j <= 7; j++)
            //    {
            //        Console.Write(solution.KInversePairs(i, j).ToString("D4") + " ");
            //    }
            //    Console.WriteLine();
            //}

            Console.WriteLine(solution.KInversePairs(1000, 1000));
            //Console.WriteLine(solution.KInversePairs(3, 1));
            //Console.WriteLine(solution.KInversePairs(10, 6));
        }
    }
}
