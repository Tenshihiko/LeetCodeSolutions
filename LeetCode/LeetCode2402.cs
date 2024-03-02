using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode2402
{
    public class Solution
    {
        public void Run()
        {
            //Console.WriteLine(MostBooked(2, [[0, 10], [1, 2], [12, 14], [13, 15]]));
            //Console.WriteLine(MostBooked(3, [[1, 20], [2, 10], [3, 5], [4, 9], [6, 8]]));
            Console.WriteLine(MostBooked(100, [[48, 49], [22, 30], [13, 31], [31, 46], [37, 46], [32, 36], [25, 36], [49, 50], [24, 34], [6, 41]]));

        }
        public int MostBooked(int n, int[][] meetings)
        {
            var m = meetings.Length;

            Array.Sort(meetings, (int[] x, int[] y) => x[0] - y[0]);

            var freeRooms = new PriorityQueue<int, int>();
            var happening = new PriorityQueue<(int, int), int>();
            var dict = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                freeRooms.Enqueue(i, i);
            }

            for (int i = 0; i < m; i++)
            {
                while(happening.Count > 0)
                {
                    var (room, end) = happening.Peek();
                    if(end <= meetings[i][0])
                    {
                        happening.Dequeue();
                        freeRooms.Enqueue(room, room);
                    } else
                    {
                        break;
                    }
                } 
                
                if (freeRooms.Count > 0)
                {
                    var room = freeRooms.Dequeue();
                    happening.Enqueue((room, meetings[i][1]), meetings[i][1]);
                    dict[room] = dict.GetValueOrDefault(room, 0) + 1;
                }
                else
                {
                    var (room, end) = happening.Dequeue();

                    if (end > meetings[i][0])
                    {
                        var length = meetings[i][1] - meetings[i][0];
                        happening.Enqueue((room, end + length), end + length);
                    } else {
                        happening.Enqueue((room, meetings[i][1]), meetings[i][1]);
                    }
                    dict[room] = dict.GetValueOrDefault(room, 0) + 1;
                }

                
            }

            int max = -1;
            int num = -1;
            foreach (var room in dict)
            {
                if (room.Value > max || room.Value == max && room.Key < num)
                {
                    max = room.Value;
                    num = room.Key;
                }
            }

            return num;
        }
    }
}
