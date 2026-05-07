using System;

namespace DailyPlannerGUI
{
    public class Task : ITask
    {
        public string Title { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCompleted { get; set; }
        
        public string GetDescription()
        {
            return $"{StartTime:HH:mm} - {EndTime:HH:mm}: {Title}";
        }
    }
}