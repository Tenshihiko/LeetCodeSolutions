using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode2709
{
    public class Solution
    {
        static List<int> Primes = new List<int>();
        static Solution()
        {
            Primes.Add(2);
            int nextPrime = 3;
            while (nextPrime < 50000)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)Primes[i] <= sqrt; i++)
                {
                    if (nextPrime % Primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    Primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
        }

        private Dictionary<int, List<int>> GetPrimeFactorsForAllNums(int[] nums)
        {
            var res = new Dictionary<int, List<int>>();
            for (int j = 0; j < nums.Length; j++)
            {
                for (int i = 0; i < Primes.Count && Primes[i] <= nums[j]; i++)
                {
                    if (nums[j] % Primes[i] == 0)
                    {
                        var list = res.GetValueOrDefault(Primes[i], new List<int>());
                        list.Add(j);
                        res[Primes[i]] = list;
                    }
                }
            }
            return res;
        }


        public void Run()
        {
            Console.WriteLine(CanTraverseAllPairs([1, 2, 3, 6]));
        }

        public bool CanTraverseAllPairs(int[] nums)
        {
            if(nums.Length == 1)
            {
                return true;
            }

            if(nums.Min() == 1)
            {
                return false;
            }

            var factors = GetPrimeFactorsForAllNums(nums);


            var djs = new DisjointSet<int>();

            for (int i = 0;i < nums.Length;i++) {
                djs.MakeSet(nums[i]);
            }

            foreach(var factor in factors)
            {
                var first = int.MaxValue;
                foreach(var num in factor.Value)
                {
                    if(first == int.MaxValue)
                    {
                        first = nums[num];
                    } else
                    {
                        djs.Union(first, nums[num]);
                    }
                }
            }

            var set = djs.FindSet(nums[0]);

            for(int i = 1; i < nums.Length;i++)
            {
                if (set != djs.FindSet(nums[i]))
                {
                    return false;
                }
            }

            return true;
        }


        class Node<T>
            where T : System.IComparable<T>
        {
            public T Data { get; set; }
            public Node<T> Parent { get; set; }
            public int Rank { get; set; }

            public Node(T data)
            {
                Data = data;
                Parent = this;
                Rank = 0;
            }
        }

        public class DisjointSet<T> : IEnumerable<T>
            where T : System.IComparable<T>
        {
            Dictionary<T, Node<T>> nodes;

            public int Count { get { return nodes.Count; } }

            public DisjointSet()
            {
                nodes = new Dictionary<T, Node<T>>();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return nodes.Keys.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return nodes.Keys.GetEnumerator();
            }

            public bool ContainsData(T data)
            {
                return nodes.ContainsKey(data);
            }

            public bool MakeSet(T data)
            {
                if (ContainsData(data))
                    return false;

                nodes.Add(data, new Node<T>(data));
                return true;
            }

            public bool Union(T dataA, T dataB)
            {
                var nodeA = nodes[dataA];
                var nodeB = nodes[dataB];

                var parentA = nodeA.Parent;
                var parentB = nodeB.Parent;

                if (parentA == parentB)
                    return false;

                if (parentA.Rank >= parentB.Rank)
                {
                    if (parentA.Rank == parentB.Rank)
                        ++parentA.Rank;

                    parentB.Parent = parentA;
                }
                else
                {
                    parentA.Parent = parentB;
                }

                return true;
            }

            public T FindSet(T data)
            {
                return FindSet(nodes[data]).Data;
            }

            public bool IsEmpty()
            {
                return Count == 0;
            }

            public void Clear()
            {
                nodes.Clear();
            }

            Node<T> FindSet(Node<T> node)
            {
                var parent = node.Parent;
                if (parent == node)
                    return node;

                node.Parent = FindSet(node.Parent);
                return node.Parent;
            }
        }
    }
}
