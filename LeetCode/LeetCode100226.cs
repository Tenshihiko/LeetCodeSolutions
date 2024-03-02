using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeetCode100226
{
    public class Solution
    {

        public void Run()
        {
            Console.WriteLine(CountPairsOfConnectableServers(
                new int[][]{ new int[] { 0, 6, 3 }, new int[]{6, 5, 3 },
                    new int[] { 0, 3, 1}, new int[]{3, 2, 7},
                    new int[]{3, 1, 6}, new int[]{3, 4, 2 }},
                3));
        }
        public int[] CountPairsOfConnectableServers(int[][] edges, int signalSpeed)
        {
            var n = edges.Length + 1;
            var g = new Dictionary<int , Dictionary<int , int>>();

            foreach (var edge in edges)
            {
                var d = g.GetValueOrDefault(edge[0], new Dictionary<int, int>());
                d.Add(edge[1], edge[2]);
                g[edge[0]] = d;

                d = g.GetValueOrDefault(edge[1], new Dictionary<int, int>());
                d.Add(edge[0], edge[2]);
                g[edge[1]] = d;
            }

            

            var res = new int[n];
            for(int i = 0; i < n; i++)
            {
                res[i] = CountPairsConnectableThoughThis(g, i, n, signalSpeed);
            }

            return res;
        }
        

        private int CountPairsConnectableThoughThis(Dictionary<int, Dictionary<int, int>> g, int root, int n, int k)
        {
            var visited = new bool[n];
            visited[root] = true;


            var connectables = new Dictionary<int, int>();
            foreach(var neib in g[root])
            {
                var q = new Queue<(int node, int dist)>();
                q.Enqueue((neib.Key, neib.Value));

                var rootEdge = neib.Value;

                while(q.Count > 0)
                {
                    var d = q.Dequeue();
                    visited[d.node] = true;

                    if(d.dist % k == 0)
                    {
                        connectables[neib.Key] = connectables.GetValueOrDefault(neib.Key) + 1;
                    }

                    foreach(var child in g[d.node])
                    {
                        if (!visited[child.Key])
                        {
                            q.Enqueue((child.Key, d.dist + child.Value));
                        }
                    }                    
                }
            }

            var count = 0;

            foreach(var conn1 in connectables)
            {
                foreach (var conn2 in connectables)
                {
                    if(conn1.Key < conn2.Key)
                    {
                        count += conn1.Value * conn2.Value;
                    }
                }
            }

            return count;
        }
    }
}
