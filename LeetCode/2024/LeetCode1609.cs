using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1609
{
    /**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */


    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    public class BinaryTreeConverter
    {
        public TreeNode ArrayToBinaryTree(int?[] nums, int index)
        {
            if (index >= nums.Length || nums[index] == null)
                return null;

            TreeNode root = new TreeNode((int)nums[index]);
            root.left = ArrayToBinaryTree(nums, 2 * index + 1);
            root.right = ArrayToBinaryTree(nums, 2 * index + 2);

            return root;
        }

        public void InorderTraversal(TreeNode root)
        {
            if (root != null)
            {
                InorderTraversal(root.left);
                Console.Write(root.val + " ");
                InorderTraversal(root.right);
            }
        }
    }

    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(IsEvenOddTree(new BinaryTreeConverter().ArrayToBinaryTree([1, 10, 4, 3, null, 7, 9, 12, 8, 6, null, null, 2], 0)));
            Console.WriteLine(IsEvenOddTree(new BinaryTreeConverter().ArrayToBinaryTree([5, 4, 2, 3, 3, 7], 0)));

        }
        public bool IsEvenOddTree(TreeNode root)
        {

            var q = new Queue<TreeNode>();

            q.Enqueue(root);

            var lvl = 0;
            while (q.Count > 0)
            {
                int n = q.Count;
                var prev = lvl % 2 == 0 ? int.MinValue : int.MaxValue;
                for (int i = 0; i < n; i++)
                {

                    var node = q.Dequeue();

                    if (lvl % 2 == 0 && node.val <= prev
                        || lvl % 2 == 1 && node.val >= prev)
                    {
                        return false;
                    }

                    if (node.val % 2 + lvl % 2 != 1)
                    {
                        return false;
                    }

                    if (node.left != null)
                    {
                        q.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        q.Enqueue(node.right);
                    }

                    prev = node.val;
                }
                lvl++;
            }

            return true;
        }
    }
}
