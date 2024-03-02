using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100232
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(MinOperations(new int[] { 1000000000, 999999999, 1000000000, 999999999, 1000000000, 999999999 }
, 1000000000));
        }
        public int MinOperations(int[] nums, int k)
        {
            var q = new PriorityQueue<long, long>();

            foreach(var i in nums)
            {
                q.Enqueue(i, i);
            }

            var count = 0;
            while (q.Count >= 2 && q.Peek() < k) 
            {
                count++;
                
                long a = q.Dequeue();
                long b = q.Dequeue();
                long c = 2 * a + b;
                q.Enqueue(c, c);
            }

            return count;
        }
    }
}
