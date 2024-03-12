using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1171
{
    public class Solution
    {

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }

            public static ListNode ListFromArray(int[] array)
            {
                var head = new ListNode(array[0]);
                var node = head;

                for (int i = 1; i < array.Length; i++)
                {
                    node.next = new ListNode(array[i]);
                    node = node.next;
                }

                return head;
            }
        }

        public void Run()
        {
            Console.WriteLine(RemoveZeroSumSublists(ListNode.ListFromArray([1, 2, 3, -3, 4])));
        }


        public ListNode RemoveZeroSumSublists(ListNode head)
        {
            var anchor = new ListNode(0, head);

            bool found;
            do
            {
                found = false;
                var node = anchor.next;

                var sums = new Dictionary<int, ListNode>();
                sums[0] = anchor;
                int sum = 0;
                while (node != null)
                {
                    sum += node.val;
                    if (sums.ContainsKey(sum))
                    {
                        var prev = sums[sum];
                        prev.next = node.next;
                        found = true;
                    }
                    else
                    {
                        sums[sum] = node;
                    }
                    node = node.next;
                }
            } while (found);

            return anchor.next;
        }
    }
}
