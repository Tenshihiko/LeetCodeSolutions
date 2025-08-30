using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100209
{
    public class Solution
    {

        public void Run()
        {
            Console.WriteLine(UnmarkedSumArray(
                [1, 2, 2, 1, 2, 3, 1],
[[1, 2], [3, 3], [4, 2]]));
        }
        public long[] UnmarkedSumArray(int[] nums, int[][] queries)
        {
            var sum = nums.Sum();

            var res = new long[queries.Length];

            var marked = new bool[nums.Length];

            var q = new PriorityQueue<int, (int, int)>();
            for (int i = 0; i < nums.Length; i++)
            {
                q.Enqueue(i, (nums[i], i));
            }

            for (int i = 0; i < queries.Length; i++)
            {

                if (!marked[queries[i][0]])
                {
                    marked[queries[i][0]] = true;
                    sum -= nums[queries[i][0]];
                }

                var k = queries[i][1];

                while (k > 0 && q.Count > 0)
                {
                    var ind = q.Dequeue();
                    if (!marked[ind])
                    {
                        marked[ind] = true;
                        k--;
                        sum -= nums[ind];
                    }
                }

                res[i] = sum;
            }

            return res;
        }
    }
}
