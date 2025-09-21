namespace LeetCode3508;

public class Solution
{
    public void Run()
    {
        List<string> commands = ["Router", "addPacket", "forwardPacket", "getCount"];
        List<List<int>> param = [[2], [2, 5, 1], [], [5, 1, 1]];

        var Router = new Router(param[0][0]);

        for (int i = 1; i < commands.Count; i++)
        {
            if (commands[i] == "addPacket")
            {
                Console.WriteLine(Router.AddPacket(param[i][0], param[i][1], param[i][2]));
            }
            else if (commands[i] == "forwardPacket")
            {
                var res = Router.ForwardPacket();
                Console.WriteLine($"[{string.Join(",", res)}]");
            }
            else if (commands[i] == "getCount")
            {
                Console.WriteLine(Router.GetCount(param[i][0], param[i][1], param[i][2]));
            }
        }
    }
    public class Router
    {
        record Packet(int source, int destination, int timestamp);
        int MemoryLimit;
        Queue<Packet> Queue;
        HashSet<Packet> Set;
        Dictionary<int, List<Packet>> DestinationMap;

        public Router(int memoryLimit)
        {
            MemoryLimit = memoryLimit;
            Queue = new Queue<Packet>();
            Set = new HashSet<Packet>();
            DestinationMap = new Dictionary<int, List<Packet>>();
        }

        public bool AddPacket(int source, int destination, int timestamp)
        {
            var packet = new Packet(source, destination, timestamp);
            var isExists = Set.Contains(packet);
            if (isExists) return false;

            if (Queue.Count == MemoryLimit)
            {
                var oldestPacket = Queue.Dequeue();
                Set.Remove(oldestPacket);
                DestinationMap[oldestPacket.destination].Remove(oldestPacket);
            }

            Queue.Enqueue(packet);
            Set.Add(packet);
            if (!DestinationMap.ContainsKey(destination))
            {
                DestinationMap[destination] = new List<Packet>();
            }
            DestinationMap[destination].Add(packet);            

            return true;
        }

        public int[] ForwardPacket()
        {
            if (Queue.Count == 0) return Array.Empty<int>();
            var oldestPacket = Queue.Dequeue();
            Set.Remove(oldestPacket);
            DestinationMap[oldestPacket.destination].Remove(oldestPacket);
            return [oldestPacket.source, oldestPacket.destination, oldestPacket.timestamp];
        }

        public int GetCount(int destination, int startTime, int endTime)
        {
            if (!DestinationMap.ContainsKey(destination) 
                || DestinationMap[destination].Count == 0) 
                return 0;

            var left = BinaryFindFirstMoreThan(DestinationMap[destination], startTime - 1);
            var right = BinaryFindFirstMoreThan(DestinationMap[destination], endTime);
            right = right == -1 ? DestinationMap[destination].Count : right;

            return left == -1 ? 0 : right - left;
        }

        private int BinaryFindFirstMoreThan(List<Packet> packets, int value)
        {
            int left = 0, right = packets.Count - 1;
            while (left < right)
            {
                int mid = left + (right - left) / 2;
                if (packets[mid].timestamp > value)
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return packets[left].timestamp > value ? left : -1;
        }
    }
}

