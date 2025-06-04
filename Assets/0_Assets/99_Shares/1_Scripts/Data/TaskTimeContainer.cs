using System;
using Util; 

namespace Attention.Data
{
    [DIPublisher]
    public class TaskTimeContainer
    {
        public float TaskTime { get; private set; }

        public TaskTimeContainer()
        {
            TaskTime = 0;
            DI.Register(this);
        }

        public void UpdateTime(float deltaTime)
        {
            TaskTime += deltaTime;
        }

        public string GetFormattedTime()
        {
            TimeSpan time = TimeSpan.FromSeconds(TaskTime);
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                                 (int)time.TotalHours,
                                 time.Minutes,
                                 time.Seconds);
        }
    }
}

