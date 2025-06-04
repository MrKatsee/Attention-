using System;
using Util; 

namespace Attention.Data
{
    [DIPublisher]
    public class TaskTimeDataContainer
    {
        public float TaskTime { get; private set; }
        public bool IsWorking { get; private set; }

        public TaskTimeDataContainer()
        {
            TaskTime = 0;
            IsWorking = false;
            DI.Register(this);
        }

        public void UpdateTime(float deltaTime)
        {
            TaskTime += deltaTime;
        }

        public void ResetTime()
        {
            TaskTime = 0;
        }

        public string GetFormattedTime()
        {
            TimeSpan time = TimeSpan.FromSeconds(TaskTime);
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                                 (int)time.TotalHours,
                                 time.Minutes,
                                 time.Seconds);
        }

        public void SetWorkingState(bool isWorking)
        {
            IsWorking = isWorking;
        }
    }
}

