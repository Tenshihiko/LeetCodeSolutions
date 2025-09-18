namespace LeetCode3408;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);
    }

    public class TaskManager
    {
        PriorityQueue<int, (int priority, int id)> TaskQueue = new PriorityQueue<int, (int priority, int id)>(
            Comparer<(int priority, int id)>.Create((a, b) =>
                                                        a.priority == b.priority
                                                        ? -a.id.CompareTo(b.id)
                                                        : -a.priority.CompareTo(b.priority)));
        Dictionary<int, (int user, int priority)> Tasks = new Dictionary<int, (int user, int priority)>();

        public TaskManager(IList<IList<int>> tasks)
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                var user = tasks[i][0];
                var task = tasks[i][1];
                var priority = tasks[i][2];

                Tasks[task] = (user, priority);
                TaskQueue.Enqueue(task, (priority, task));
            }
        }

        public void Add(int userId, int taskId, int priority)
        {
            TaskQueue.Enqueue(taskId, (priority, taskId));
            Tasks[taskId] = (userId, priority);
        }

        public void Edit(int taskId, int newPriority)
        {
            var (user, _) = Tasks[taskId];
            Tasks[taskId] = (user, newPriority);
            TaskQueue.Enqueue(taskId, (newPriority, taskId));
        }

        public void Rmv(int taskId)
        {
            Tasks.Remove(taskId);
        }

        public int ExecTop()
        {
            while (TaskQueue.TryPeek(out var taskId, out var queuePriority))
            {
                TaskQueue.Dequeue();
                if (Tasks.ContainsKey(taskId) && Tasks[taskId].priority == queuePriority.priority)
                {
                    var user = Tasks[taskId].user;
                    Tasks.Remove(taskId);
                    return user;
                }
            }
            return -1;
        }
    }
}