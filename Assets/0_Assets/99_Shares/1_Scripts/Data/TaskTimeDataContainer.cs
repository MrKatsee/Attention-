using System;
using Util; 

namespace Attention.Data
{
    [DIPublisher]
    public class TaskTimeDataContainer
    {
        private const float MULTIPLIER = 1.0f;
        public float TaskTime { get; private set; }
        public int PastMoney { get; private set; }
        public int CurrentMoney { get; private set; }



        public TaskTimeDataContainer()
        {
            TaskTime = 0;
            DI.Register(this);
        }

        public void UpdateTime(float deltaTime)
        {
            TaskTime += deltaTime;
            CurrentMoney = (int)((int)TaskTime * MULTIPLIER);
        }

        public void UpdateMoney()
        {
            PastMoney = CurrentMoney;
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


    }
}

