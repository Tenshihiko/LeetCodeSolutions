using LeetCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LeetCode1171.Solution;

namespace LeetCode143
{
    public class Solution
    {
        public void Run()
        {
            ReorderList(ListNode.ListFromArray([1, 2, 3, 4, 5]));
        }

        public ListNode Middle(ListNode head)
        {
            var root = new ListNode(0, head);
            var slow = root;
            var fast = root;

            while (fast != null)
            {
                fast = fast.next?.next;
                slow = slow.next;
            }

            return slow;
        }

        public void ReorderList(ListNode head)
        {
            var middle = Middle(head);

            var stack = new Stack<ListNode>();

            while (middle != null)
            {
                stack.Push(middle);
                middle = middle.next;
            }

            var curr = head;
            while (stack.Count > 0 && curr != null)
            {
                var next = curr.next;
                curr.next = stack.Pop();
                curr.next.next = next;
                curr = next;
            }

            //return head;
        }
    }
}
