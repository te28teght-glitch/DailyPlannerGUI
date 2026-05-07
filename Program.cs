using System;
using System.Windows.Forms;

namespace DailyPlannerGUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            var form = new Form1();
            form.TaskManager.LoadFromFile("tasks.json");
            form.RefreshTasks();
            form.FormClosing += (s, e) => form.TaskManager.SaveToFile("tasks.json");
            
            Application.Run(form);
        }
    }
}