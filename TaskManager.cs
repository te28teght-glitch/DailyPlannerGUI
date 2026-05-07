using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json;

namespace DailyPlannerGUI
{
    public class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public void SaveToFile(string fileName)
        {
            var get = GetAllTasks();
            string json = JsonSerializer.Serialize(get);
            File.WriteAllText(fileName,json);
        }

        public void LoadFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                string json = File.ReadAllText(filename);
                var loadedTasks = JsonSerializer.Deserialize<List<Task>>(json);
                if (loadedTasks != null)
                {
                    tasks = loadedTasks;
                }

            }
        }
 
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