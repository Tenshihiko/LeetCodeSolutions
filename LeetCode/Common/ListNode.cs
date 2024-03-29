using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Common
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
}
