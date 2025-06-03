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

    }
}

