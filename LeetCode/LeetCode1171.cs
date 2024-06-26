﻿using LeetCode.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1171
{
    public class Solution
    {



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
