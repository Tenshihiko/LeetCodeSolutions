using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100243
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(ResultArray(new int[] { 2, 1, 3 }));
        }

        
        public int[] ResultArray(int[] nums)
        {
            var arr1 = new List<int>();
            var arr2 = new List<int>();
            arr1.Add(nums[0]);
            arr2.Add(nums[1]);

            int last1 = nums[0];
            int last2 = nums[1];

            for (int i = 2; i < nums.Length; i++)
            {
                if (last1 > last2)
                {
                    arr1.Add(nums[i]);
                    last1 = nums[i];
                }
                else
                {
                    arr2.Add(nums[i]);
                    last2 = nums[i];
                }
            }
            arr1.AddRange(arr2);
            return arr1.ToArray();
        }        
    }
}
