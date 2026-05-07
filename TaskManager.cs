using System;
using System.Collections.Generic;

namespace DailyPlanner
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();
        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public List<Task> GetAllTasks()
        {
            return tasks;
        }
        public bool RemoveTask(int index)
        {
            if (index >= 0 && index < tasks.Count)
            {
              tasks.RemoveAt(index);
              return true;
            }
            return false;
        }
        public int GetTaskCount()
        {
            return tasks.Count;
        }
    }
}