namespace LeetCode4;

public class Solution
{
    public void Run()
    {
        // var param = ...
        // var result = YourMethod(param);
    }

    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int n1 = nums1.Length, n2 = nums2.Length;
        int n = n1 + n2;

        if (n % 2 == 1)
        {
            return KthSmallest(n / 2, nums1, nums2, 0, n1 - 1, 0, n2 - 1);
        }
        else
        {
            return (double)(KthSmallest(n / 2, nums1, nums2, 0, n1 - 1, 0, n2 - 1) +
                            KthSmallest(n / 2 - 1, nums1, nums2, 0, n1 - 1, 0, n2 - 1)) /
                   2;
        }
    }

    private int KthSmallest(int k, int[] a, int[] b, int a_start, int a_end, int b_start, int b_end)
    {
        if (a_start > a_end)
        {
            return b[k - a_start];
        }

        if (b_start > b_end)
        {
            return a[k - b_start];
        }

        var a_index = (a_start + a_end) / 2;
        var b_index = (b_start + b_end) / 2;
        var a_value = a[a_index];
        var b_value = b[b_index];

        if (a_index + b_index < k)
        {
            if (a_value > b_value)
            {
                return KthSmallest(k, a, b, a_start, a_end, b_index + 1, b_end);
            }
            else
            {
                return KthSmallest(k, a, b, a_index + 1, a_end, b_start, b_end);
            }
        }
        else
        {
            if (a_value > b_value)
            {
                return KthSmallest(k, a, b, a_start, a_index - 1, b_start, b_end);
            }
            else
            {
                return KthSmallest(k, a, b, a_start, a_end, b_start, b_index - 1);
            }
        }
    }
}

