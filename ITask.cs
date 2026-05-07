using System;

namespace DailyPlanner
{
    public interface ITask
    {
        string Title { get; set; }
        DateTime StartTime { get; set; }
        DateTime EndTime { get; set; }
        bool IsCompleted { get; set; }
    }
}