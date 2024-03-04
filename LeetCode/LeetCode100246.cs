using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100246
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(ResultArray(new int[] { 44, 90, 48, 26, 96, 96, 18, 92, 3, 
                88, 95, 91, 36, 78, 13, 27, 21, 79, 45, 58, 3, 46, 26, 16, 
                43, 12, 90, 1, 16, 94, 54, 87, 73, 87, 17, 86, 69, 13, 78, 
                8, 89, 36, 83, 96, 47, 16, 38, 50, 13, 8 }));
        }

        public class DuplicateKeyComparer<TKey>
                :
             IComparer<TKey> where TKey : IComparable
        {
            #region IComparer<TKey> Members

            public int Compare(TKey x, TKey y)
            {
                int result = x.CompareTo(y);

                if (result == 0)
                    return 1; // Handle equality as being greater. Note: this will break Remove(key) or
                else          // IndexOfKey(key) since the comparer never returns 0 to signal key equality
                    return result;
            }

            #endregion
        }

        private int GreaterCount(SortedList<int, int> arr, int val)
        {
            if(arr.Count == 0) return 0;

            int index = BinarySearch(arr, val);
            if(index >= arr.Count) return 0;

            if (arr.Keys[index] == val)
            {
                return arr.Count - index - 1;
            }
            else
            {
                return arr.Count - index;
            }
        }

        static int BinarySearch(SortedList<int, int> arr, int val)
        {
            int left = 0;
            int right = arr.Count - 1;
            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                if (arr.Keys[mid] <= val)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return left;
        }


        public int[] ResultArray(int[] nums)
        {
            var arr1 = new List<int>();
            var sorted1 = new SortedList<int, int>(new DuplicateKeyComparer<int>());
            var arr2 = new List<int>();
            var sorted2 = new SortedList<int, int>(new DuplicateKeyComparer<int>());
            arr1.Add(nums[0]);
            sorted1.Add(nums[0], nums[0]);
            arr2.Add(nums[1]);
            sorted2.Add(nums[1], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                var g1 = GreaterCount(sorted1, nums[i]);
                var g2 = GreaterCount(sorted2, nums[i]);

                if (g1 > g2)
                {
                    arr1.Add(nums[i]);
                    sorted1.Add(nums[i], nums[i]);
                }
                else if (g2 > g1)
                {
                    arr2.Add(nums[i]);
                    sorted2.Add(nums[i], nums[i]);
                }
                else if (arr2.Count < arr1.Count)
                {
                    arr2.Add(nums[i]);
                    sorted2.Add(nums[i], nums[i]);
                }
                else
                {
                    arr1.Add(nums[i]);
                    sorted1.Add(nums[i], nums[i]);
                }
            }
            arr1.AddRange(arr2);
            return arr1.ToArray();
        }
    }
}
